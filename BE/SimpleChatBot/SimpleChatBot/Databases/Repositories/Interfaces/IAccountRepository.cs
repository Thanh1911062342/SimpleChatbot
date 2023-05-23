using SimpleChatBot.Databases.Entities;

namespace SimpleChatBot.Databases.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Task<Account> GetAccountByEmail(string email);

        public Task<bool> Create(Account account);

        public Task<bool> Update(Account account);

        public Task<IEnumerable<Account>> GetAllAccounts();
    }
}
