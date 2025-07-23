using DatabaseBroker.DataContext;
using Entity.Models;
using Entity.Models.Auth;

namespace DatabaseBroker.Repositories.Auth;

public class TokenRepository(IhdaDataContext dbContext)
    : RepositoryBase<TokenModel, long>(dbContext), ITokenRepository;