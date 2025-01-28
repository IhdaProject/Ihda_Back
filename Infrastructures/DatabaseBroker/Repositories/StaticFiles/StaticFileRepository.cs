using DatabaseBroker.DataContext;
using Entity.Models.StaticFiles;

namespace DatabaseBroker.Repositories.StaticFiles;

public class StaticFileRepository(IhdaDataContext dbContext)
    : RepositoryBase<StaticFile, long>(dbContext), IStaticFileRepository;