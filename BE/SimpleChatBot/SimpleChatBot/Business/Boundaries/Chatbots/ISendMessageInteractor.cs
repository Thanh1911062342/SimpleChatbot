using SimpleChatBot.Databases.Dtos;

namespace SimpleChatBot.Business.Boundaries.Chatbots
{
    public interface ISendMessageInteractor
    {
        public class Request
        {
            public string Jwt { get; set; }
            public string Message { get; set; }

            public Request(string jwt, string message)
            {
                Jwt = jwt;
                Message = message;
            }
        }

        public class Response
        {
            public string Message { get; set;}
            public bool IsJWTValid { get; set; }
            public bool IsSuccess { get; set; }

            public Response(string message, bool isJWTValid, bool isSuccess)
            {
                Message = message;
                IsJWTValid = isJWTValid;
                IsSuccess = isSuccess;
            }
        }

        public Task<Response> SendMessage(Request request);
    }
}
