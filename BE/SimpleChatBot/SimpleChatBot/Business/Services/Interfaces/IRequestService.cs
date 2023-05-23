using SimpleChatBot.Business.Boundaries;
using SimpleChatBot.Business.Boundaries.Accounts;
using SimpleChatBot.Databases.Dtos.Accounts;

namespace SimpleChatBot.Business.Services.Interfaces
{
    public interface IRequestService
    {
        public Task<long> CalculateLimitTimeToGetKey(string email, string ip, int limitedTime);
    }
}
