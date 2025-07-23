using Entity.DataTransferObjects.StaticFiles;

namespace StaticFileService.Service;

public interface IStaticFileService
{
    Task<StaticFileDto> AddFileAsync(FileDto fileDto);
    Task<StaticFileDto> RemoveAsync(RemoveFileDto removeFileDto);
}