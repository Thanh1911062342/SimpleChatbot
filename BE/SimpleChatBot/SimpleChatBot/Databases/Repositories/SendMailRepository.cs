using Microsoft.EntityFrameworkCore;
using SimpleChatBot.Databases.Entities;
using SimpleChatBot.Databases.Repositories.Interfaces;

namespace SimpleChatBot.Databases.Repositories
{
    public class SendMailRepository : ISendMailRepository
    {
        private readonly ChatbotContext _chatbotContext;

        public SendMailRepository(ChatbotContext chatbotContext)
        {
            _chatbotContext = chatbotContext;
        }


        public async Task<SendMail> GetLastEmailSentByAccountId(int accountId)
        {
            var lastMail = await (from sm in _chatbotContext.SendMails
                           where
                           (
                                sm.AccountId == accountId
                           )
                           orderby sm.Id descending
                           select sm).FirstOrDefaultAsync();

            return lastMail;
        }

        public async Task<IEnumerable<SendMail>> GetAllMails()
        {
            return await (from m in _chatbotContext.SendMails
                          select m).ToListAsync() ;
        }

        public async Task<SendMail> GetLastEmailSentByIp(string ip)
        {
            var lastMail = await (from sm in _chatbotContext.SendMails
                                  where
                                  (
                                       sm.Ip.Equals(ip)
                                  )
                                  orderby sm.Id descending
                                  select sm).FirstOrDefaultAsync();

            return lastMail;
        }

        public async Task<bool> Create(SendMail sendMail)
        {
            if (sendMail == null) return false;

            try
            {
                _chatbotContext.SendMails.Add(sendMail);
                await _chatbotContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
