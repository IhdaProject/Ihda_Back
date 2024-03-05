using DatabaseBroker.DataContext;
using Entity.Models;

namespace DatabaseBroker.Repositories.Auth;

public class PermissionRepository : RepositoryBase<Permission, long>,IPermissionRepository
{
    public PermissionRepository(ProgramDataContext dbContext) : base(dbContext)
    {
    }
}
