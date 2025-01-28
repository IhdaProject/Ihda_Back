using DatabaseBroker.Repositories.StaticFiles;
using Entity.DataTransferObjects.StaticFiles;
using Entity.Exeptions;
using Entity.Models.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace StaticFileService.Service;

public class StaticFileService(IStaticFileRepository staticFileRepository) : IStaticFileService
{
    public async Task<StaticFileDto> AddFileAsync(FileDto fileDto)
    {
        var filePath = Guid.NewGuid() + Path.GetExtension(fileDto.File.FileName);
        var fieldName = fileDto.FieldName;
        if(fieldName.Length == 0)
            fieldName = "temp";
        
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot", fieldName);
        
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        path = Path.Combine(path, filePath);

        var staticFile = new StaticFile()
        {
            Path = path,
            FileExtension = Path.GetExtension(fileDto.File.FileName),
            Url = $"{fieldName}/{filePath}",
            Size = fileDto.File.Length,
            OldName = fileDto.FileName ?? Path.GetFileName(fileDto.File.FileName)
        };

        await using Stream fileStream = new FileStream(path, FileMode.Create);
        await  fileDto.File.CopyToAsync(fileStream);

        staticFile = await staticFileRepository.AddAsync(staticFile);

        return new StaticFileDto(staticFile.Id,staticFile.Url,staticFile.OldName);
    }
    public async Task<StaticFileDto> RemoveAsync(RemoveFileDto removeFileDto)
    {
        var staticFile = await staticFileRepository.GetAllAsQueryable()
            .FirstOrDefaultAsync(sf => sf.Url == removeFileDto.FilePath);
        
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            staticFile.Path);
        
        if (staticFile == null || staticFile.Id == 0)
            throw new NotFoundException($"Static File Not found by url: {removeFileDto.FilePath}");

        await staticFileRepository.RemoveAsync(staticFile);

        return new StaticFileDto(staticFile.Id, staticFile.Url,staticFile.OldName);
    }
}