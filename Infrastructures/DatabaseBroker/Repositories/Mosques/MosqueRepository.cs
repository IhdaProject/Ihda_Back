using DatabaseBroker.DataContext;
using Entity.Models.Mosques;

namespace DatabaseBroker.Repositories.Mosques;

public class MosqueRepository(IhdaDataContext dbContext) : RepositoryBase<Mosque, long>(dbContext), IMosqueRepository;