using System.Net;
using AutoMapper;
using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.Files;
using Entity.DataTransferObjects.QuranCourses;
using Entity.Enums;
using Entity.Exceptions;
using Entity.Models.ApiModels;
using Entity.Models.QuranCourses;
using Microsoft.EntityFrameworkCore;
using MinIoBroker.Services;
using WebCore.Extensions;

namespace QuranCourseService.Services;

public class TrainingCenterService(
    GenericRepository<TrainingCenter, long> trainingCenterRepository,
    GenericRepository<CourseForm, long> courseFormRepository,
    IMinioService minioService,
    IMapper mapper) : ITrainingCenterService
{
    public async Task<ResponseModel<TrainingCenterDto>> OnSaveTrainingCenterAsync(TrainingCenterDto trainingCenter, long userId)
    {
        if (trainingCenter.Id == 0)
        {
            var newEntity = await trainingCenterRepository.AddWithSaveChangesAsync(
                MapTrainingCenter(trainingCenter, [..trainingCenter.PhotosLink?.Select(pl => pl.DbUrl) ?? []]));
            
            newEntity.CreatedBy = userId;
            
            return ResponseModel<TrainingCenterDto>.ResultFromContent(
                MapTrainingCenterDto(
                    newEntity),
                HttpStatusCode.Created);
        }

        var entity = await trainingCenterRepository.GetByIdAsync(trainingCenter.Id) ?? throw new NotFoundException($"Not found {nameof(trainingCenter)}");
        MapTrainingCenter(trainingCenter,[..trainingCenter.PhotosLink?.Select(pl => pl.DbUrl) ?? []],entity);
        entity.UpdatedBy  = userId;
        await trainingCenterRepository.UpdateWithSaveChangesAsync(entity);

        return ResponseModel<TrainingCenterDto>.ResultFromContent(MapTrainingCenterDto(entity));
    }
    private static TrainingCenterDto MapTrainingCenterDto(TrainingCenter trainingCenter, List<FileItemDto>? fileItem = null, long? userId =  null)
    {
        return new TrainingCenterDto(
            trainingCenter.Id,
            userId > 0 ? trainingCenter.Id.EncryptId("trainingcenterid", userId.Value) : "",
            trainingCenter.Name,
            trainingCenter.Description,
            trainingCenter.Address,
            trainingCenter.Landmark,
            trainingCenter.PhoneNumber,
            fileItem,
            trainingCenter.WorkingHours,
            trainingCenter.Latitude,
            trainingCenter.Longitude,
            trainingCenter.DistrictId,
            userId > 0 && trainingCenter.CenterManagers.Count > 0
                ? trainingCenter.CenterManagers.Select(cm => cm.UserId.EncryptId("userid", userId.Value)).ToList()
                : []);
    }
    private static TrainingCenter MapTrainingCenter(TrainingCenterDto trainingCenterDto, string[] files, TrainingCenter? trainingCenter = null, long? userId = null)
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
        if (!(userId > 0)) return result;
        
        var newUserIds = trainingCenterDto.EncryptUserIds.DecryptId("userid", userId.Value);
        result.CenterManagers ??= [];

        foreach (var manager in result.CenterManagers.Where(m => !newUserIds.Contains(m.UserId)))
            result.CenterManagers.Remove(manager);
            
        var existingIds = result.CenterManagers.Select(m => m.UserId);
        foreach (var id in newUserIds.Where(id => !existingIds.Contains(id)))
            result.CenterManagers.Add(new CenterManager { UserId = id });
        
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

    public async Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCentersByManagerAsync(MetaQueryModel metaQuery, long userId)
    {
        var query = trainingCenterRepository.GetAllAsQueryable()
            .Where(tc => tc.CenterManagers.Any(m => m.UserId == userId))
            .FilterByExpressions(metaQuery);

        var items = await query
            .Sort(metaQuery)
            .Paging(metaQuery)
            .Select(c => MapTrainingCenterDto(
                c, null, userId))
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
                result, [..result.PhotosLink?.Select(pl => new FileItemDto(pl, minioService.GetPresignedUrlAsync(pl).Result)) ?? []]));
    }
    public async Task<ResponseModel<CourseFormDto>> OnSaveCourseFormAsync(CourseFormDto courseForm, long userId)
    {
        if (courseForm.Id == 0)
            return ResponseModel<CourseFormDto>.ResultFromContent(
                mapper.Map<CourseFormDto>(
                    await courseFormRepository.AddWithSaveChangesAsync(
                        mapper.Map<CourseForm>(courseForm))),HttpStatusCode.Created);

        var entity = await courseFormRepository.GetByIdAsync(courseForm.Id) ?? throw new NotFoundException($"Not found {nameof(courseForm)}");
        mapper.Map(courseForm, entity);
        entity.UpdatedBy = userId;
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

    public async Task<ResponseModel<List<CourseFormByManagerDto>>> GetCourseFormsByTrainingCenterAsync(MetaQueryModel metaQuery, long trainingCenterId, long userId)
    {
        var query = courseFormRepository.GetAllAsQueryable()
            .Where(cf => cf.TrainingCenterId == trainingCenterId)
            .FilterByExpressions(metaQuery);

        var items = await query
            .Sort(metaQuery)
            .Paging(metaQuery)
            .Select(c => new CourseFormByManagerDto(
                c.Id,
                c.Id.EncryptId("courseformid", userId),
                c.Name,
                c.Description,
                c.AdmissionQuota,
                c.Duration,
                c.ForGender,
                c.TransitionTime,
                c.Petitions.Count(p => p.Status == QuranCoursePetitionStatus.New)))
            .ToListAsync();

        var totalCount = await query.CountAsync();

        return ResponseModel<List<CourseFormByManagerDto>>.ResultFromContent(
            items,
            total: totalCount);
    }

    public async Task<ResponseModel<CourseFormDto>> GetCourseFormByIdAsync(long id)
        => ResponseModel<CourseFormDto>.ResultFromContent(
            mapper.Map<CourseFormDto>(await courseFormRepository.GetByIdAsync(id)));
}