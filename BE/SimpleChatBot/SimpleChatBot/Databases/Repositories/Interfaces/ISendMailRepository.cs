using SimpleChatBot.Databases.Entities;

namespace SimpleChatBot.Databases.Repositories.Interfaces
{
    public interface ISendMailRepository
    {
        public Task<SendMail> GetLastEmailSentByAccountId(int accountId);
        public Task<IEnumerable<SendMail>> GetAllMails();
        public Task<SendMail> GetLastEmailSentByIp(string ip);
        public Task<bool> Create(SendMail message);
    }
}
