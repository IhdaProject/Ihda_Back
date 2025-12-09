using System.Net;
using AutoMapper;
using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.Files;
using Entity.DataTransferObjects.QuranCourses;
using Entity.Exceptions;
using Entity.Models.ApiModels;
using Entity.Models.QuranCourses;
using Microsoft.EntityFrameworkCore;
using MinIoBroker.Services;

namespace QuranCourseService.Services;

public class TrainingCenterService(
    GenericRepository<TrainingCenter, long> trainingCenterRepository,
    GenericRepository<CourseForm, long> courseFormRepository,
    IMinioService minioService,
    IMapper mapper) : ITrainingCenterService
{
    public async Task<ResponseModel<TrainingCenterDto>> OnSaveTrainingCenterAsync(TrainingCenterDto trainingCenter)
    {
        if (trainingCenter.Id == 0)
        {
            var newEntity = await trainingCenterRepository.AddWithSaveChangesAsync(
                MapTrainingCenter(trainingCenter, [..trainingCenter.PhotosLink?.Select(pl =>
                    minioService.UploadFileFromFormFileAsync(pl.File, $"{Guid.NewGuid()}{Path.GetExtension(pl.File.FileName)}").Result) ?? []]));
            
            return ResponseModel<TrainingCenterDto>.ResultFromContent(
                MapTrainingCenterDto(
                    newEntity),
                HttpStatusCode.Created);
        }

        var entity = await trainingCenterRepository.GetByIdAsync(trainingCenter.Id) ?? throw new NotFoundException($"Not found {nameof(trainingCenter)}");
        MapTrainingCenter(trainingCenter, await UpdateTrainingCenterPhotos(entity.PhotosLink, trainingCenter.PhotosLink ?? []),entity);
        await trainingCenterRepository.UpdateWithSaveChangesAsync(entity);

        return ResponseModel<TrainingCenterDto>.ResultFromContent(MapTrainingCenterDto(entity));
    }
    public async Task<string[]> UpdateTrainingCenterPhotos(
        string[] photosLink,
        List<FileItemDto> incomingPhotos)
    {
        var incomingDbUrls = incomingPhotos
            .Where(x => !string.IsNullOrEmpty(x.DbUrl))
            .Select(x => x.DbUrl)
            .ToList();
        
        var toDelete = photosLink.Except(incomingDbUrls).ToList();

        foreach (var deleteUrl in toDelete)
            await minioService.DeleteFileAsync(deleteUrl);
        
        var toUpload = incomingPhotos
            .Where(x => x.File != null && string.IsNullOrEmpty(x.DbUrl))
            .ToList();

        var uploadedUrls = new List<string>();

        foreach (var item in toUpload)
        {
            var newUrl = await minioService.UploadFileFromFormFileAsync(item.File, $"{Guid.NewGuid()}{Path.GetExtension(item.File.FileName)}");
            uploadedUrls.Add(newUrl);
        }

        return incomingDbUrls.Concat(uploadedUrls).ToArray();
    }
    private TrainingCenterDto MapTrainingCenterDto(TrainingCenter trainingCenter, List<FileItemDto>? fileItem = null)
    {
        return new TrainingCenterDto(
            trainingCenter.Id,
            trainingCenter.Name,
            trainingCenter.Description,
            trainingCenter.Address,
            trainingCenter.Landmark,
            trainingCenter.PhoneNumber,
            fileItem,
            trainingCenter.WorkingHours,
            trainingCenter.Latitude,
            trainingCenter.Longitude,
            trainingCenter.DistrictId);
    }
    private TrainingCenter MapTrainingCenter(TrainingCenterDto trainingCenterDto, string[] files, TrainingCenter? trainingCenter = null)
    {
        var result = trainingCenter ?? new TrainingCenter();
        result.Id = trainingCenterDto.Id;
        result.Name = trainingCenterDto.Name;
        result.Description = trainingCenterDto.Description;
        result.Address = trainingCenterDto.Address;
        result.Landmark = trainingCenterDto.Landmark;
        result.PhoneNumber = trainingCenterDto.PhoneNumber;
        result.PhotosLink = files;
        result.WorkingHours = trainingCenterDto.WorkingHours;
        result.Latitude = trainingCenterDto.Latitude;
        result.Longitude = trainingCenterDto.Longitude;
        result.DistrictId = trainingCenterDto.DistrictId;
        return result;
    }
    public async Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCentersAsync(MetaQueryModel metaQuery)
    {
        var query = trainingCenterRepository.GetAllAsQueryable()
            .FilterByExpressions(metaQuery);

        var items = await query
            .Sort(metaQuery)
            .Paging(metaQuery)
            .Select(c => MapTrainingCenterDto(
                c, null))
            .ToListAsync();

        var totalCount = await query.CountAsync();

        return ResponseModel<List<TrainingCenterDto>>.ResultFromContent(
            items,
            total: totalCount);
    }
    public async Task<ResponseModel<TrainingCenterDto>> GetTrainingCenterByIdAsync(long id)
    {
        var result = await trainingCenterRepository.GetByIdAsync(id)
            ??  throw new NotFoundException($"Not found {nameof(TrainingCenter)}");
        
        return ResponseModel<TrainingCenterDto>.ResultFromContent(
            MapTrainingCenterDto(
                result, [..result.PhotosLink.Select(pl => new FileItemDto(null, pl, minioService.GetPresignedUrlAsync(pl).Result))]));
    }
    public async Task<ResponseModel<CourseFormDto>> OnSaveCourseFormAsync(CourseFormDto courseForm)
    {
        if (courseForm.Id == 0)
            return ResponseModel<CourseFormDto>.ResultFromContent(
                mapper.Map<CourseFormDto>(
                    await courseFormRepository.AddWithSaveChangesAsync(
                        mapper.Map<CourseForm>(courseForm))),HttpStatusCode.Created);

        var entity = await courseFormRepository.GetByIdAsync(courseForm.Id) ?? throw new NotFoundException($"Not found {nameof(courseForm)}");
        mapper.Map(courseForm, entity);
        await courseFormRepository.UpdateWithSaveChangesAsync(entity);

        return ResponseModel<CourseFormDto>.ResultFromContent(mapper.Map<CourseFormDto>(entity));
    }
    public async Task<ResponseModel<List<CourseFormDto>>> GetCourseFormsAsync(MetaQueryModel metaQuery)
    {
        var query = courseFormRepository.GetAllAsQueryable()
            .FilterByExpressions(metaQuery);

        var items = await query
            .Sort(metaQuery)
            .Paging(metaQuery)
            .Select(c => mapper.Map<CourseFormDto>(c))
            .ToListAsync();

        var totalCount = await query.CountAsync();

        return ResponseModel<List<CourseFormDto>>.ResultFromContent(
            items,
            total: totalCount);
    }
    public async Task<ResponseModel<CourseFormDto>> GetCourseFormByIdAsync(long id)
        => ResponseModel<CourseFormDto>.ResultFromContent(
            mapper.Map<CourseFormDto>(await courseFormRepository.GetByIdAsync(id)));
}