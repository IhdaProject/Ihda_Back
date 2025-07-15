using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.Mosques;
using Entity.DataTransferObjects.PrayerTimes;
using Entity.Exceptions;
using Entity.Models.ApiModels;
using Entity.Models.Mosques;
using Microsoft.EntityFrameworkCore;
using WebCore.Models;

namespace MosqueService.Services;

public class MosqueService(GenericRepository<Mosque,long> mosqueRepository,
    GenericRepository<MosquePrayerTime, long> prayerTimeRepository) : IMosqueService
{
    public async Task<ResponseModel<List<MosqueByListDto>>> GetListAsync(MetaQueryModel queryModel)
    {
        var query = mosqueRepository.GetAllAsQueryable().Where(m => !m.IsDelete);

        return new ResponseModel<List<MosqueByListDto>>(await query.Skip(queryModel.Skip).Take(queryModel.Take)
            .Select(m => new MosqueByListDto(m.Id, m.Name, m.Latitude, m.Longitude)).ToListAsync())
            {
                Total = await query.CountAsync()
            };
    }
    public Task<ResponseModel<List<MosqueByListDto>>> GetFavoriteListAsync(MetaQueryModel queryModel, long userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseModel<MosqueWithTimeDto>> GetByIdAsync(long id)
    {
        var mosque = await mosqueRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Not found Mosque by Id: {id}");
        
        return new ResponseModel<MosqueWithTimeDto>(new MosqueWithTimeDto(mosque.Id, mosque.Name, mosque.Description, mosque.PhotoUrls,
            mosque.Latitude, mosque.Longitude, null));
    }

    public async Task<ResponseModel<MosqueDto>> OnSaveMosqueAsync(MosqueDto mosqueDto, long id, long userId)
    {
        Mosque mosque;
        if (mosqueDto.Id == 0)
        {
            mosque = await mosqueRepository.AddWithSaveChangesAsync(new Mosque()
            {
                Name = mosqueDto.Name,
                Description = mosqueDto.Description,
                PhotoUrls = mosqueDto.PhotoUrls,
                Latitude = mosqueDto.Latitude,
                Longitude = mosqueDto.Longitude
            });
        }
        else
        {
            mosque = await mosqueRepository.GetByIdAsync(mosqueDto.Id) ?? throw new NotFoundException($"Not found Mosque by Id: {mosqueDto.Id}");
            
            mosque.Name = mosqueDto.Name;
            mosque.Description = mosqueDto.Description;
            mosque.PhotoUrls = mosqueDto.PhotoUrls;
            mosque.Latitude = mosqueDto.Latitude;
            mosque.Longitude = mosqueDto.Longitude;

            await mosqueRepository.UpdateAsync(mosque);
        }
        
        return new ResponseModel<MosqueDto>(new MosqueDto(mosque.Id, mosque.Name, mosque.Description, mosque.PhotoUrls,mosque.Latitude, mosque.Longitude));
    }

    public async Task<ResponseModel<MosquePrayerTimeDto>> OnSavePrayerTimeAsync(MosquePrayerTimeDto mosquePrayerTimeDto,long id ,long userId)
    {
        MosquePrayerTime mosquePrayerTime;
        if (mosquePrayerTimeDto.Id == 0)
        {
            mosquePrayerTime = await prayerTimeRepository.AddWithSaveChangesAsync(new MosquePrayerTime()
            {
                AdhamFajr = mosquePrayerTimeDto.AdhamFajr,
                AdhamDhuhr = mosquePrayerTimeDto.AdhamDhuhr,
                AdhamAsr = mosquePrayerTimeDto.AdhamAsr,
                AdhamMaghrib = mosquePrayerTimeDto.AdhamMaghrib,
                AdhamIsha = mosquePrayerTimeDto.AdhamIsha,
                IqamahFajr = mosquePrayerTimeDto.IqamahFajr,
                IqamahDhuhr = mosquePrayerTimeDto.IqamahDhuhr,
                IqamahAsr = mosquePrayerTimeDto.IqamahAsr,
                IqamahMaghrib = mosquePrayerTimeDto.IqamahMaghrib,
                IqamahIsha = mosquePrayerTimeDto.IqamahIsha,
                CreatedBy = userId
            });
        }
        else
        {
            mosquePrayerTime = await prayerTimeRepository.GetByIdAsync(mosquePrayerTimeDto.Id) ?? throw new NotFoundException($"Not found Mosque by Id: {mosquePrayerTimeDto.Id}");
            
            mosquePrayerTime.AdhamFajr = mosquePrayerTimeDto.AdhamFajr;
            mosquePrayerTime.AdhamDhuhr = mosquePrayerTimeDto.AdhamDhuhr;
            mosquePrayerTime.AdhamAsr = mosquePrayerTimeDto.AdhamAsr;
            mosquePrayerTime.AdhamMaghrib = mosquePrayerTimeDto.AdhamMaghrib;
            mosquePrayerTime.AdhamIsha = mosquePrayerTimeDto.AdhamIsha;
            mosquePrayerTime.IqamahFajr = mosquePrayerTimeDto.IqamahFajr;
            mosquePrayerTime.IqamahDhuhr = mosquePrayerTimeDto.IqamahDhuhr;
            mosquePrayerTime.IqamahAsr = mosquePrayerTimeDto.IqamahAsr;
            mosquePrayerTime.IqamahMaghrib = mosquePrayerTimeDto.IqamahMaghrib;
            mosquePrayerTime.IqamahIsha = mosquePrayerTimeDto.IqamahIsha;

            await prayerTimeRepository.UpdateAsync(mosquePrayerTime);
        }
        
        return new ResponseModel<MosquePrayerTimeDto>(mosquePrayerTimeDto with { Id = mosquePrayerTime.Id, MosqueId = mosquePrayerTime.MosqueId });
    }

    public Task<ResponseModel<bool>> ToggleFavoriteAsync(long id, long userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseModel<List<MosqueByListDto>>> GetMyListAsync(MetaQueryModel metaQuery, long userId)
    {
        var query = mosqueRepository.GetAllAsQueryable().Where(m => m.MosqueAdmins.Any(ma => ma.UserId == userId));

        return new ResponseModel<List<MosqueByListDto>>(await query.Skip(metaQuery.Skip).Take(metaQuery.Take)
            .Select(m => new MosqueByListDto(m.Id, m.Name, m.Latitude, m.Longitude)).ToListAsync())
        {
            Total = await query.CountAsync()
        };
    }
}