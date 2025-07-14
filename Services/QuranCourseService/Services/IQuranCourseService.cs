using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.ApiModels;
using WebCore.Models;

namespace QuranCourseService.Services;

public interface IQuranCourseService
{
    Task<ResponseModel<PetitionForQuranCourseDto>> CreatePetitionAsync(PetitionForQuranCourseDto petition);
    Task<ResponseModel<List<PetitionForQuranCourseByListDto>>> GetAllPetitionsAsync(MetaQueryModel metaQueryModel);
    Task<ResponseModel<PetitionInfosForQuranCourseDto>> GetPetitionInfoWithTimeAsync(GetPetitionInfoDto petitionInfoDto);
}