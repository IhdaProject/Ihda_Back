using System.Net;
using AutoMapper;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.QuranCourses;
using Entity.Enums;
using Entity.Exceptions;
using Entity.Models.QuranCourses;
using Microsoft.EntityFrameworkCore;
using WebCore.Models;

namespace QuranCourseService.Services;

public class QuranCourseService(GenericRepository<PetitionForQuranCourse, long> petitionForQuranCourseRepository,
    IMapper mapper) : IQuranCourseService
{
    public async Task<ResponseModel<PetitionForQuranCourseDto>> CreatePetitionAsync(PetitionForQuranCourseDto petition)
    {
        if (await petitionForQuranCourseRepository.GetAllAsQueryable()
                .Where(p => p.Pinfl == petition.Pinfl)
               // .Where(p => p.Status == QuranCoursePetitionStatus.New)
                .AnyAsync())
            throw new AlreadyExistsException("There is already an application.");
        
        return ResponseModel<PetitionForQuranCourseDto>.ResultFromContent(
            mapper.Map<PetitionForQuranCourseDto>(
                await petitionForQuranCourseRepository.AddWithSaveChangesAsync(
                    mapper.Map<PetitionForQuranCourse>(petition))),HttpStatusCode.Created);
    }
}