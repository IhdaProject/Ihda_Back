using DatabaseBroker.DataContext;
using Entity.Models;

namespace DatabaseBroker.Repositories.Auth;

public class UserRepository(IhdaDataContext dbContext) : RepositoryBase<User, long>(dbContext), IUserRepository;