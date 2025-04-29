using Entity.Models;
using Entity.Models.Auth;

namespace DatabaseBroker.Repositories.Auth;

public interface ITokenRepository : IRepositoryBase<TokenModel,long>
{
}