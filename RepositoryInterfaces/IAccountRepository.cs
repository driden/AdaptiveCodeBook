using Domain;

namespace RepositoryInterfaces
{
    public interface IAccountRepository
    {
        Account GetByName(string accountName);
    }
}
