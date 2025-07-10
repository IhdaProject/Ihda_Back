using System.Net;
using AutoMapper;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.QuranCourses;
using Entity.Exceptions;
using Entity.Models.QuranCourses;
using Microsoft.EntityFrameworkCore;
using WebCore.Models;

namespace QuranCourseService.Services;

public class QuranCourseService(GenericRepository<PetitionForQuranCourse, long> petitionForQuranCourseRepository,
    GenericRepository<CourseForm, long> courseFormRepository,
    IMapper mapper) : IQuranCourseService
{
    public async Task<ResponseModel<PetitionForQuranCourseDto>> CreatePetitionAsync(PetitionForQuranCourseDto petition)
    {
        if (petition.PassportExpireDate < DateTime.Now)
            throw new ValidationException("This passport expired");
        
        if (await petitionForQuranCourseRepository.GetAllAsQueryable()
                .Where(p => p.Pinfl == petition.Pinfl)
                .AnyAsync())
            throw new AlreadyExistsException("There is already an application.");
        
        var courseForm = await courseFormRepository.GetByIdAsync(petition.CourseFormId)
            ?? throw new NotFoundException("Course form not found");

        var yearsOld = (DateTime.Now - petition.BirthDay).Days / 365.0;
        if (courseForm.MinYearsOld > yearsOld || courseForm.MaxYearsOld < yearsOld)
            throw new ValidationException("This years old problem");
        
        if(courseForm.ForGender is not null && courseForm.ForGender != petition.Gender)
            throw new ValidationException("This course form is for gender");
        
        return ResponseModel<PetitionForQuranCourseDto>.ResultFromContent(
            mapper.Map<PetitionForQuranCourseDto>(
                await petitionForQuranCourseRepository.AddWithSaveChangesAsync(
                    mapper.Map<PetitionForQuranCourse>(petition))),HttpStatusCode.Created);
    }
}