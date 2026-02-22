using Entity.DataTransferObjects.QuranCourses;
using Entity.Enums;
using Entity.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using QuranCourseService.Services;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Extensions;

namespace QuranCourseApi.Controllers;

public class QuranCourseController(IQuranCourseService quranCourseService) : ApiControllerBase
{
    [HttpPost]
    [ApiGroup("Client")]
    public async Task<ResponseModel<PetitionForQuranCourseDto>> CreatePetition([FromBody]PetitionForCreatedQuranCourseDto petition)
        => await quranCourseService.CreatePetitionAsync(petition);
    [HttpGet]
    [ApiGroup("Client")]
    public async Task<ResponseModel<PetitionInfosForQuranCourseDto>> GetPetitionInfo([FromQuery]GetPetitionInfoDto petitionInfoDto)
        => await quranCourseService.GetPetitionInfoWithTimeAsync(petitionInfoDto);
    [ApiGroup("Admin")]
    [HttpGet, PermissionAuthorize(UserPermissions.ViewAllPetitionQuranCourses)]
    public async Task<ResponseModel<List<PetitionForQuranCourseByListDto>>> GetAllPetitions([FromQuery]MetaQueryModel metaQueryModel)
        => await quranCourseService.GetAllPetitionsAsync(metaQueryModel);
    [ApiGroup("Admin")]
    [HttpGet, PermissionAuthorize(UserPermissions.ViewPetitionQuranCoursesForManager)]
    public async Task<ResponseModel<List<PetitionForQuranCourseByListDto>>> GetCourseFormPetitions([FromQuery]MetaQueryModel metaQueryModel, [FromRoute]string courseFormId)
        => await quranCourseService.GetCourseFormPetitionsAsync(metaQueryModel, courseFormId.DecryptId("courseformid", UserId));
}