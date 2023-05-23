using Microsoft.EntityFrameworkCore;
using SimpleChatBot.Databases.Entities;
using SimpleChatBot.Databases.Repositories.Interfaces;

namespace SimpleChatBot.Databases.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ChatbotContext _chatbotContext;

        public AccountRepository(ChatbotContext chatbotContext)
        {
            _chatbotContext = chatbotContext;
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            var account = await (from e in _chatbotContext.Accounts
                                 where
                                 
                                     e.Email.Equals(email)
                                 
                                 select e).FirstOrDefaultAsync();

            return account;
        }

        public async Task<bool> Create(Account account)
        {
            if (account == null)
            {
                return false;
            }

            if (await GetAccountByEmail(account.Email) != null)
            {
                return false;
            }

            try
            {
                _chatbotContext.Accounts.Add(account);
                await _chatbotContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(Account account)
        {
            if (account == null)
            {
                return false;
            }

            if ((await GetAccountByEmail(account.Email)) == null)
            {
                return false;
            }

            try
            {
                _chatbotContext.Accounts.Update(account);
                await _chatbotContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await (from a in _chatbotContext.Accounts
                         orderby a.Id ascending
                         select a).ToListAsync();
        }
    }
}
