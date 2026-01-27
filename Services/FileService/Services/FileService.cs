using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.Files;
using Entity.Models.ApiModels;
using MinIoBroker.Services;
using File = Entity.Models.Clouds.File;

namespace FileService.Services;

public class FileService(
    GenericRepository<File, long> fileRepository,
    IMinioService minioService) : IFileService
{
    public async Task<ResponseModel<FileItemDto>> UploadFileAsync(FileDto fileDto)
    {
        var dbUrl = await minioService.UploadFileFromFormFileAsync(fileDto.File, fileDto.File.FileName);

        await fileRepository.AddAsync(new File()
        {
            ObjectName = dbUrl
        });
        
        return ResponseModel<FileItemDto>.ResultFromContent(
            new FileItemDto(
                dbUrl,
                await minioService.GetPresignedUrlAsync(dbUrl)));
    }
}