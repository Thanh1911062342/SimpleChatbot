using SimpleChatBot.Databases.Dtos;
using SimpleChatBot.Databases.Dtos.Accounts;

namespace SimpleChatBot.Business.Boundaries.Accounts
{
    public interface ICheckSpamGetKeyInteractor
    {
        public class Request
        {
            public string Email;
            public string Ip;

            public Request(string email, string ip)
            {
                Email = email;
                Ip = ip;
            }
        }

        public class Response
        {
            public long UnixTimestamp { get; set; }

            public Response(long unixtimestamp)
            {
                UnixTimestamp = unixtimestamp;
            }
        }

        public Task<Response> CheckSpam(Request request);
    }
}