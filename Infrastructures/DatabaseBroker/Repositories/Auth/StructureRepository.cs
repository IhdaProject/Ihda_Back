using DatabaseBroker.DataContext;
using Entity.Models;

namespace DatabaseBroker.Repositories.Auth;

public class StructureRepository(IhdaDataContext dbContext)
    : RepositoryBase<Structure, long>(dbContext), IStructureRepository;