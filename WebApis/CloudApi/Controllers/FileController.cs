using Entity.DataTransferObjects.Files;
using Entity.Enums;
using Entity.Models.ApiModels;
using FileService.Services;
using Microsoft.AspNetCore.Mvc;
using WebCore.Attributes;
using WebCore.Controllers;

namespace CloudApi.Controllers;
public class FileController(IFileService fileService) : ApiControllerBase
{
    [HttpPost]
    [ApiGroup("Client", "Admin", "Mobile")]
    [PermissionAuthorize(UserPermissions.UploadFile)]
    public async Task<ResponseModel<FileItemDto>> Upload([FromForm] FileDto fileDto)
        => await fileService.UploadFileAsync(fileDto);
}