using DatabaseBroker.DataContext;
using Entity.Models.Auth;

namespace DatabaseBroker.Repositories.Auth;

public class SignMethodsRepository : RepositoryBase<SignMethod, long>, ISignMethodsRepository
{
    public SignMethodsRepository(ProgramDataContext dbContext) : base(dbContext)
    {
    }
}