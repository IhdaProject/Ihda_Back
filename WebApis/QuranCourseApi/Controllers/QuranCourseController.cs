using Entity.DataTransferObjects.QuranCourses;
using Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using QuranCourseService.Services;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Models;

namespace QuranCourseApi.Controllers;

public class QuranCourseController(IQuranCourseService quranCourseService) : ApiControllerBase
{
    [HttpPost]
    [ApiGroup("Client")]
    public async Task<ResponseModel<PetitionForQuranCourseDto>> CreatePetition([FromBody]PetitionForQuranCourseDto petition)
        => await quranCourseService.CreatePetitionAsync(petition);
    [HttpGet]
    [ApiGroup("Client")]
    public async Task<ResponseModel<PetitionInfosForQuranCourseDto>> GetPetitionInfo([FromQuery]GetPetitionInfoDto petitionInfoDto)
        => await quranCourseService.GetPetitionInfoWithTimeAsync(petitionInfoDto);
    [ApiGroup("Admin")]
    [HttpGet, PermissionAuthorize(UserPermissions.ViewPetitionQuranCourses)]
    public async Task<ResponseModel<List<PetitionForQuranCourseByListDto>>> GetAllPetitions([FromQuery]MetaQueryModel metaQueryModel)
        => await quranCourseService.GetAllPetitionsAsync(metaQueryModel);
}