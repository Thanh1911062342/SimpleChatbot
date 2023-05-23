using SimpleChatBot.Databases.Dtos;

namespace SimpleChatBot.Business.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendMail(EmailDto email, int maxRetryCount);
    }
}
