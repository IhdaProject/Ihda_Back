using DatabaseBroker.Repositories.Mosques;
using Entity.DataTransferObjects.Mosques;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.Mosques;
using Microsoft.EntityFrameworkCore;
using WebCore.Models;

namespace MosqueService.Services;

public class MosqueService(IMosqueRepository mosqueRepository) : IMosqueService
{
    public async Task<ResponseModel<List<MosqueByListDto>>> GetListAsync(MetaQueryModel queryModel)
    {
        var query = mosqueRepository.Where(m => !m.IsDelete);

        return new ResponseModel<List<MosqueByListDto>>(await query.Skip(queryModel.Skip).Take(queryModel.Take)
            .Select(m => new MosqueByListDto(m.Id, m.Name, m.Latitude, m.Longitude)).ToListAsync())
            {
                Total = await query.CountAsync()
            };
    }

    public Task<ResponseModel<List<MosqueByListDto>>> GetFavoriteListAsync(MetaQueryModel queryModel)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseModel<MosqueDto>> GetByIdAsync(long id)
    {
        var mosque = await mosqueRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Not found Mosque by Id: {id}");
        
        return new ResponseModel<MosqueDto>(new MosqueDto(mosque.Id, mosque.Name, mosque.Description, mosque.PhotoUrls,
            mosque.Latitude, mosque.Longitude));
    }

    public async Task<ResponseModel<MosqueDto>> OnSaveAsync(MosqueDto mosqueDto)
    {
        Mosque mosque;
        if (mosqueDto.Id == 0)
        {
            mosque = await mosqueRepository.AddAsync(new Mosque()
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

    public Task<ResponseModel<bool>> ToggleFavoriteAsync(long id, long userId)
    {
        throw new NotImplementedException();
    }
}