using DatabaseBroker.DataContext;
using Entity.Models;

namespace DatabaseBroker.Repositories.Auth;

public class StructurePermissionRepository : RepositoryBase<StructurePermission, long>, IStructurePermissionRepository
{
    public StructurePermissionRepository(ProgramDataContext dbContext) : base(dbContext)
    {
    }
}