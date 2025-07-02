using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.ApiModels;
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
}