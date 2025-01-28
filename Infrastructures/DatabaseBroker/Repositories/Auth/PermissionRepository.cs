using DatabaseBroker.DataContext;
using Entity.Models;
using Entity.Models.Auth;

namespace DatabaseBroker.Repositories.Auth;

public class PermissionRepository(IhdaDataContext dbContext)
    : RepositoryBase<Permission, long>(dbContext), IPermissionRepository;
