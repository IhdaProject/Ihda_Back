using Entity.DataTransferObjects.StaticFiles;
using Microsoft.AspNetCore.Mvc;
using StaticFileService.Service;
using WebCore.Models;

namespace AssetApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[RequestFormLimits(MultipartBodyLengthLimit = (long)1024 * 1024 * 1024 * 1024)]
public class StaticFileController : ControllerBase       
{
    private readonly IStaticFileService _staticFileService;

    public StaticFileController(IStaticFileService staticFileService)
    {
        _staticFileService = staticFileService;
    }

    [HttpPost]
    public async Task<ResponseModel> Add([FromForm]FileDto fileDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _staticFileService.AddFileAsync(fileDto));
    }

    [HttpDelete]
    public async Task<ResponseModel> Remove([FromBody]RemoveFileDto removeFileDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _staticFileService.RemoveAsync(removeFileDto));
    }
}