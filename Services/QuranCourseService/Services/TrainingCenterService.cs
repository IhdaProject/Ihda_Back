using System.Net;
using AutoMapper;
using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.QuranCourses;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.QuranCourses;
using Microsoft.EntityFrameworkCore;
using WebCore.Models;

namespace QuranCourseService.Services;

public class TrainingCenterService(
    GenericRepository<TrainingCenter, long> trainingCenterRepository,
    GenericRepository<CourseForm, long> courseFormRepository,
    IMapper mapper) : ITrainingCenterService
{
    public async Task<ResponseModel<TrainingCenterDto>> OnSaveTrainingCenterAsync(TrainingCenterDto trainingCenter)
    {
        if (trainingCenter.Id == 0)
            return ResponseModel<TrainingCenterDto>.ResultFromContent(
                mapper.Map<TrainingCenterDto>(
                    await trainingCenterRepository.AddWithSaveChangesAsync(
                        mapper.Map<TrainingCenter>(trainingCenter))),HttpStatusCode.Created);

        var entity = await trainingCenterRepository.GetByIdAsync(trainingCenter.Id) ?? throw new NotFoundException($"Not found {nameof(trainingCenter)}");
        mapper.Map(trainingCenter, entity);
        await trainingCenterRepository.UpdateWithSaveChangesAsync(entity);

        return ResponseModel<TrainingCenterDto>.ResultFromContent(mapper.Map<TrainingCenterDto>(entity));
    }
    public async Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCentersAsync(MetaQueryModel metaQuery)
    {
        var query = trainingCenterRepository.GetAllAsQueryable()
            .FilterByExpressions(metaQuery);

        var items = await query
            .Sort(metaQuery)
            .Paging(metaQuery)
            .Select(c => mapper.Map<TrainingCenterDto>(c))
            .ToListAsync();

        var totalCount = await query.CountAsync();

        return ResponseModel<List<TrainingCenterDto>>.ResultFromContent(
            items,
            total: totalCount);
    }
    public async Task<ResponseModel<TrainingCenterDto>> GetTrainingCenterByIdAsync(long id)
        => ResponseModel<TrainingCenterDto>.ResultFromContent(
            mapper.Map<TrainingCenterDto>(await trainingCenterRepository.GetByIdAsync(id)));
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