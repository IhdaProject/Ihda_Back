using DatabaseBroker.DataContext;
using Entity.Models;

namespace DatabaseBroker.Repositories.Auth;

public class TokenRepository : RepositoryBase<TokenModel, long>, ITokenRepository
{
    public TokenRepository(ProgramDataContext dbContext) : base(dbContext)
    {
    }
}