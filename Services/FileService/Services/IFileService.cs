using Entity.DataTransferObjects.Files;
using Entity.Models.ApiModels;

namespace FileService.Services;

public interface IFileService
{
    Task<ResponseModel<FileItemDto>> UploadFileAsync(FileDto fileDto);
}