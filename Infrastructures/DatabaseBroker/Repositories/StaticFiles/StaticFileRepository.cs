using DatabaseBroker.DataContext;
using Entity.Models.StaticFiles;

namespace DatabaseBroker.Repositories.StaticFiles;

public class StaticFileRepository : RepositoryBase<StaticFile, long> , IStaticFileRepository
{
    public StaticFileRepository(ProgramDataContext dbContext) : base(dbContext)
    {
    }
}