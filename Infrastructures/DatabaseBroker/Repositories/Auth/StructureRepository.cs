using DatabaseBroker.DataContext;
using Entity.Models;

namespace DatabaseBroker.Repositories.Auth;

public class StructureRepository : RepositoryBase<Structure, long>, IStructureRepository
{
    public StructureRepository(ProgramDataContext dbContext) : base(dbContext)
    {
    }
}