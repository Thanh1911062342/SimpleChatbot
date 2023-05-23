using SimpleChatBot.Databases.Dtos;
using SimpleChatBot.Databases.Dtos.Accounts;

namespace SimpleChatBot.Business.Boundaries.Accounts
{
    public interface ILoginInteractor
    {
        public class Request
        {
            public LoginDto LoginDto { get; set; }

            public Request(LoginDto loginDto)
            {
                LoginDto = loginDto;
            }
        }

        public class Response
        {
            public NotificationDto Notification { get; set; }

            public Response(NotificationDto notification)
            {
                Notification = notification;
            }
        }

        public Task<Response> Login(Request request); 
    }
}
