using SimpleChatBot.Databases.Dtos;
using SimpleChatBot.Databases.Dtos.Accounts;

namespace SimpleChatBot.Business.Boundaries.Accounts
{
    public interface IKeyActiveInteractor
    {
        public class Request
        {
            public string Email { get; set; }
            public string Ip { get; set; }

            public Request(string email, string ip)
            {
                Email = email;
                Ip = ip;
            }
        }

        public class Response
        {
            public NotificationDto Notification { get; set; }
            public long unixTimestamp { get; set; }

            public Response(NotificationDto notificationDto, long unixTimestamp)
            {
                Notification = notificationDto;
                this.unixTimestamp = unixTimestamp;
            }

            public Response(NotificationDto notificationDto)
            {
                Notification = notificationDto;
            }
        }

        public Task<Response> KeyActive(Request request);
    }
}
