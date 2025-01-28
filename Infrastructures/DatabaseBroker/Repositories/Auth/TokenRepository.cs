using DatabaseBroker.DataContext;
using Entity.Models;

namespace DatabaseBroker.Repositories.Auth;

public class TokenRepository(IhdaDataContext dbContext)
    : RepositoryBase<TokenModel, long>(dbContext), ITokenRepository;