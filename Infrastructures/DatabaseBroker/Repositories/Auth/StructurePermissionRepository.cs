using DatabaseBroker.DataContext;
using Entity.Models;

namespace DatabaseBroker.Repositories.Auth;

public class StructurePermissionRepository(IhdaDataContext dbContext)
    : RepositoryBase<StructurePermission, long>(dbContext), IStructurePermissionRepository;