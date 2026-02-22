using System.Net;
using AutoMapper;
using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.QuranCourses;
using Entity.Enums;
using Entity.Exceptions;
using Entity.Models.ApiModels;
using Entity.Models.QuranCourses;
using Microsoft.EntityFrameworkCore;

namespace QuranCourseService.Services;

public class QuranCourseService(GenericRepository<PetitionForQuranCourse, long> petitionForQuranCourseRepository,
    GenericRepository<CourseForm, long> courseFormRepository,
    IMapper mapper) : IQuranCourseService
{
    public async Task<ResponseModel<PetitionForQuranCourseDto>> CreatePetitionAsync(PetitionForCreatedQuranCourseDto petition)
    {
        if (petition.PassportExpireDate < DateTime.UtcNow)
            throw new ValidationException("This passport expired");
        
        if (await petitionForQuranCourseRepository.GetAllAsQueryable()
                .Where(p => p.Pinfl == petition.Pinfl)
                .AnyAsync())
            throw new AlreadyExistsException("There is already an application.");
        
        var courseForm = await courseFormRepository.GetByIdAsync(petition.CourseFormId)
            ?? throw new NotFoundException("Course form not found");

        var yearsOld = (DateTime.UtcNow - petition.BirthDay).Days / 365.0;
        if (courseForm.MinYearsOld > yearsOld || courseForm.MaxYearsOld < yearsOld)
            throw new ValidationException("This years old problem");
        
        if(courseForm.ForGender is not null && courseForm.ForGender != petition.Gender)
            throw new ValidationException("This course form is for gender");
        
        return ResponseModel<PetitionForQuranCourseDto>.ResultFromContent(
            mapper.Map<PetitionForQuranCourseDto>(
                await petitionForQuranCourseRepository.AddWithSaveChangesAsync(
                    mapper.Map<PetitionForQuranCourse>(petition))),HttpStatusCode.Created);
    }

    public async Task<ResponseModel<List<PetitionForQuranCourseByListDto>>> GetAllPetitionsAsync(MetaQueryModel metaQueryModel)
    {
        var query = petitionForQuranCourseRepository.GetAllAsQueryable()
            .FilterByExpressions(metaQueryModel);
        
        return ResponseModel<List<PetitionForQuranCourseByListDto>>.ResultFromContent(
            await query
                .Sort(metaQueryModel)
                .Paging(metaQueryModel)
                .Select(p => mapper.Map<PetitionForQuranCourseByListDto>(p))
                .ToListAsync(),
            total: await query.CountAsync());
    }

    public async Task<ResponseModel<List<PetitionForQuranCourseByListDto>>> GetCourseFormPetitionsAsync(MetaQueryModel metaQueryModel, long courseFormId)
    {
        var query = petitionForQuranCourseRepository
            .GetAllAsQueryable()
            .Where(p => p.CourseFormId == courseFormId)
            .FilterByExpressions(metaQueryModel);
        
        return ResponseModel<List<PetitionForQuranCourseByListDto>>.ResultFromContent(
            await query
                .Sort(metaQueryModel)
                .Paging(metaQueryModel)
                .Select(p => mapper.Map<PetitionForQuranCourseByListDto>(p))
                .ToListAsync(),
            total: await query.CountAsync());
    }

    public async Task<ResponseModel<PetitionInfosForQuranCourseDto>> GetPetitionInfoWithTimeAsync(GetPetitionInfoDto petitionInfoDto)
    {
        var petition = await petitionForQuranCourseRepository.GetAllAsQueryable()
            .Where(p => p.Pinfl == petitionInfoDto.Pinfl)
            .Where(p => p.Passport == petitionInfoDto.Passport || p.Id == petitionInfoDto.Id)
            .FirstOrDefaultAsync() ?? throw new NotFoundException("There is no petition found");

        var oldQueue = await petitionForQuranCourseRepository.GetAllAsQueryable()
            .Where(p => p.Id <= petition.Id)
            .CountAsync();
        
        var date = DateTime.UtcNow;
        
        var courseForm = await courseFormRepository.GetAllAsQueryable()
            .Where(cf => cf.Id == petition.CourseFormId)
            .FirstOrDefaultAsync() ?? throw new NotFoundException("There is no course form found");

        var teachers = await courseFormRepository.GetAllAsQueryable()
            .Where(cf => cf.Id == petition.CourseFormId)
            .SelectMany(cf => cf.Teachers
                .Select(t => new {
                    StartDate = t.Courses.Where(c => c.Status == CourseStatus.Progress)
                        .Select(c => c.StartDate.AddDays(cf.Duration))
                        .FirstOrDefault(date),
                    t.User.FullName,
                }))
            .OrderBy(cft => cft.StartDate)
            .ToListAsync();
        
        if(teachers.Count == 0)
            throw new ValidationException("There is no teachers found");
        
        teachers = teachers.Select(t =>
            t with { StartDate = t.StartDate.AddDays(courseForm.Duration * oldQueue / (teachers.Count * courseForm.AdmissionQuota)) }).ToList();

        var teacher = teachers.Skip(oldQueue - (teachers.Count * courseForm.AdmissionQuota) *
                (oldQueue / (teachers.Count * courseForm.AdmissionQuota)))
            .Take(1)
            .FirstOrDefault();

        return ResponseModel<PetitionInfosForQuranCourseDto>.ResultFromContent(
            new PetitionInfosForQuranCourseDto(
                petition.Id,
                petition.FullName,
                petition.PhoneNumber,
                petition.Status,
                petition.BirthDay, 
                teacher.StartDate - date));
    }
}