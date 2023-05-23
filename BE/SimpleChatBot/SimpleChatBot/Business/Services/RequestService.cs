using SimpleChatBot.Business.Boundaries.Accounts;
using SimpleChatBot.Business.Services.Interfaces;
using SimpleChatBot.Databases.Dtos;
using SimpleChatBot.Databases.Dtos.Accounts;
using SimpleChatBot.Databases.Repositories.Interfaces;

namespace SimpleChatBot.Business.Services
{
    public class RequestService : IRequestService
    {
        private readonly ISendMailRepository _sendMailRepository;
        private readonly IAccountRepository _accountRepository;

        public RequestService(ISendMailRepository sendMailRepository, IAccountRepository accountRepository)
        {
            _sendMailRepository = sendMailRepository;
            _accountRepository = accountRepository;
        }

        public async Task<long> CalculateLimitTimeToGetKey(string email, string ip, int limitedTime)
        {

            DateTimeOffset now = DateTimeOffset.Now;
            long unixTimestampNow = now.ToUnixTimeSeconds();

            var mail = await _sendMailRepository.GetLastEmailSentByIp(ip);
            if (mail != null)
            {
                if (unixTimestampNow - (mail.Timestamp != null ? mail.Timestamp.Value : 0) <= limitedTime)
                {
                    return limitedTime - (unixTimestampNow - (mail.Timestamp != null ? mail.Timestamp.Value : 0));
                }
            }


            mail = await _accountRepository.GetAccountByEmail(email) != null ? 
                    await _sendMailRepository.GetLastEmailSentByAccountId((await _accountRepository.GetAccountByEmail(email)).Id) : 
                    null;
            if (mail != null)
            {
                if (unixTimestampNow - (mail.Timestamp != null ? mail.Timestamp.Value : 0) <= limitedTime)
                {
                    return limitedTime - (unixTimestampNow - (mail.Timestamp != null ? mail.Timestamp.Value : 0));
                }
            }

            return 0;
        }
    }
}
