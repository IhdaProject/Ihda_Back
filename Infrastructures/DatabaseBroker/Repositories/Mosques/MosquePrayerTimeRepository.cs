using DatabaseBroker.DataContext;
using Entity.Models.Mosques;

namespace DatabaseBroker.Repositories.Mosques;

public class MosquePrayerTimeRepository(IhdaDataContext dbContext) : RepositoryBase<MosquePrayerTime, long>(dbContext), IMosquePrayerTimeRepository;