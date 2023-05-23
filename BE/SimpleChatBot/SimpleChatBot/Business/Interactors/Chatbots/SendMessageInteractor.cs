using SimpleChatBot.Business.Boundaries.Chatbots;
using SimpleChatBot.Databases.Dtos;

namespace SimpleChatBot.Business.Interactors.Chatbots
{
    public class SendMessageInteractor : ISendMessageInteractor
    {
        public async Task<ISendMessageInteractor.Response> SendMessage(ISendMessageInteractor.Request request)
        {
            if (string.IsNullOrEmpty(request.Jwt))
            {
                NotificationDto notification = new NotificationDto()
                {
                    Message = "Không tìm thấy tài khoản, vui lòng đăng nhập lại",
                    Status = false
                };

                return new ISendMessageInteractor.Response("", false, notification);
            }


        }
    }
}
