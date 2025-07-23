using Entity.DataTransferObjects.StaticFiles;
using Microsoft.AspNetCore.Mvc;
using StaticFileService.Service;
using WebCore.Attributes;
using WebCore.Models;

namespace AssetApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[RequestFormLimits(MultipartBodyLengthLimit = (long)1024 * 1024 * 1024 * 1024)]
[ApiGroup("Client")]
public class StaticFileController(IStaticFileService staticFileService) : ControllerBase
{
    [HttpPost]
    public async Task<ResponseModel<StaticFileDto>> Add([FromForm]FileDto fileDto)
        => ResponseModel<StaticFileDto>
            .ResultFromContent(await staticFileService.AddFileAsync(fileDto));

    /*[HttpDelete]
    public async Task<ResponseModel<StaticFileDto>> Remove([FromBody]RemoveFileDto removeFileDto)
        => ResponseModel<StaticFileDto>
            .ResultFromContent(
                await staticFileService.RemoveAsync(removeFileDto));*/
}