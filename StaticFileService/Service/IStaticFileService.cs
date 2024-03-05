using Entity.DataTransferObjects.StaticFiles;

namespace StaticFileService.Service;

public interface IStaticFileService
{
    ValueTask<StaticFileDto> AddFileAsync(FileDto fileDto);
    ValueTask<StaticFileDto> RemoveAsync(RemoveFileDto removeFileDto);
}