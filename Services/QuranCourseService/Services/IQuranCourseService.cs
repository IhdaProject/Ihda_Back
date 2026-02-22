using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.ApiModels;

namespace QuranCourseService.Services;

public interface IQuranCourseService
{
    Task<ResponseModel<PetitionForQuranCourseDto>> CreatePetitionAsync(PetitionForQuranCourseDto petition);
    Task<ResponseModel<List<PetitionForQuranCourseByListDto>>> GetAllPetitionsAsync(MetaQueryModel metaQueryModel);
    Task<ResponseModel<PetitionInfosForQuranCourseDto>> GetPetitionInfoWithTimeAsync(GetPetitionInfoDto petitionInfoDto);
    Task<ResponseModel<List<PetitionForQuranCourseByListDto>>> GetCourseFormPetitionsAsync(MetaQueryModel metaQueryModel, long decryptId);
}