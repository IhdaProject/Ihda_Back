using DatabaseBroker.DataContext;
using Entity.Models;
using Entity.Models.Auth;

namespace DatabaseBroker.Repositories.Auth;

public class StructurePermissionRepository(IhdaDataContext dbContext)
    : RepositoryBase<StructurePermission, long>(dbContext), IStructurePermissionRepository;