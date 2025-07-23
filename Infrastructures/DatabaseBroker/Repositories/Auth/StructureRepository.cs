using DatabaseBroker.DataContext;
using Entity.Models;
using Entity.Models.Auth;

namespace DatabaseBroker.Repositories.Auth;

public class StructureRepository(IhdaDataContext dbContext)
    : RepositoryBase<Structure, long>(dbContext), IStructureRepository;