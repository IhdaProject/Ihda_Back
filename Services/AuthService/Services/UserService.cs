using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories;
using DatabaseBroker.Repositories.Auth;
using Entity.DataTransferObjects.Authentication;
using Entity.DataTransferObjects.Users;
using Entity.Exceptions;
using Entity.Models.ApiModels;
using Entity.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services;

public class UserService(IUserRepository userRepository,
    GenericRepository<UserStructure, long> userStructureRepository) :  IUserService
{
    public async Task<ResponseModel<List<UserDto>>> GetUsersAsync(MetaQueryModel metaQueryModel)
    {
        var query = userRepository
            .GetAllAsQueryable()
            .FilterByExpressions(metaQueryModel);

        var items = await query
            .Sort(metaQueryModel)
            .Paging(metaQueryModel)
            .Select(u => new UserDto(
                u.Id,
                u.FullName,
                u.Structures.Select(s => s.StructureId).ToList()))
            .ToListAsync();
        
        return ResponseModel<List<UserDto>>.ResultFromContent(items,total: await query.CountAsync());
    }
    public async Task<ResponseModel<UserFullDto>> GetUserByIdAsync(long userId)
    {
        var user = await userRepository.GetByIdAsync(userId)
            ?? throw new NotFoundException("Not found user");

        return ResponseModel<UserFullDto>.ResultFromContent(new UserFullDto(user.Id,
            user.FullName, []));
    }

    public async Task<ResponseModel<bool>> AddStructureAsync(ChangeUserStructureDto userStructure)
    {
        if(await userStructureRepository.GetAllAsQueryable()
            .Where(us => us.UserId == userStructure.UserId)
            .Where(us => us.StructureId == userStructure.StructureId)
            .AnyAsync())
            throw new AlreadyExistsException("Structure already exists");

        await userStructureRepository.AddWithSaveChangesAsync(new UserStructure()
        {
            UserId = userStructure.UserId,
            StructureId = userStructure.StructureId,
        });

        return true;
    }
    public async Task<ResponseModel<bool>> RemoveStructureAsync(ChangeUserStructureDto userStructure)
    {
        var userStructureModel = await userStructureRepository.GetAllAsQueryable()
               .Where(us => us.UserId == userStructure.UserId)
               .Where(us => us.StructureId == userStructure.StructureId)
               .FirstOrDefaultAsync() ?? throw new NotFoundException("Not found structure");

        await userStructureRepository.RemoveWithSaveChangesAsync(userStructureModel.Id);

        return true;
    }
}