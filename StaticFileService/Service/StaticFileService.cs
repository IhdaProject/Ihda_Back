using DatabaseBroker.Repositories.StaticFiles;
using Entity.DataTransferObjects.StaticFiles;
using Entity.Exeptions;
using Entity.Models.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace StaticFileService.Service;

public class StaticFileService : IStaticFileService
{
    private readonly IStaticFileRepository _staticFileRepository;

    public StaticFileService(IStaticFileRepository staticFileRepository)
    {
        _staticFileRepository = staticFileRepository;
    }
    public async ValueTask<StaticFileDto> AddFileAsync(FileDto fileDto)
    {
        var filePath = Guid.NewGuid() + Path.GetExtension(fileDto.file.FileName);
        var fieldName = fileDto.fieldName;
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
            FileExtension = Path.GetExtension(fileDto.file.FileName),
            Url = $"{fieldName}/{filePath}",
            Size = fileDto.file.Length,
            OldName = fileDto.fileName ?? Path.GetFileName(fileDto.file.FileName)
        };

        await using Stream fileStream = new FileStream(path, FileMode.Create);
        await  fileDto.file.CopyToAsync(fileStream);

        staticFile = await _staticFileRepository.AddAsync(staticFile);

        return new StaticFileDto(staticFile.Id,staticFile.Url,staticFile.OldName);
    }
    public async ValueTask<StaticFileDto> RemoveAsync(RemoveFileDto removeFileDto)
    {
        var staticFile = await _staticFileRepository.GetAllAsQueryable()
            .FirstOrDefaultAsync(sf => sf.Url == removeFileDto.filePath);
        
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            staticFile.Path);
        
        if (staticFile == null || staticFile.Id == 0)
        {
            throw new NotFoundException($"Static File Not found by url: {removeFileDto.filePath}");
        }

        await _staticFileRepository.RemoveAsync(staticFile);

        return new StaticFileDto(staticFile.Id, staticFile.Url,staticFile.OldName);
    }
}