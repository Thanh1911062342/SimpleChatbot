using SimpleChatBot.Business.Boundaries.Chatbots;
using SimpleChatBot.Business.Services.Interfaces;
using SimpleChatBot.Databases.Dtos;

namespace SimpleChatBot.Business.Interactors.Chatbots
{
    public class SendMessageInteractor : ISendMessageInteractor
    {
        private readonly IEncodeService _encodeService;
        private readonly IChatBotService _chatBotService;

        public SendMessageInteractor(IEncodeService encodeService, 
                                    IChatBotService chatBotService)
        {
            _encodeService = encodeService;
            _chatBotService = chatBotService;
        }

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

            if (!(await _encodeService.ValidateJwt(request.Jwt)))
            {
                NotificationDto notification = new NotificationDto()
                {
                    Message = "Tài khoản hết hiệu lực, vui lòng đăng nhập lại",
                    Status = false
                };

                return new ISendMessageInteractor.Response("", false, notification);
            }

            var responseMessage = await _chatBotService.GetMessage(request.Message);
            if (responseMessage == null)
            {
                NotificationDto notification = new NotificationDto()
                {
                    Message = "Bị lỗi vui lòng thử lại sau",
                    Status = false
                };

                return new ISendMessageInteractor.Response("", true, notification);
            }

            return new ISendMessageInteractor.Response(responseMessage, true, new NotificationDto() { });
        }
    }
}
