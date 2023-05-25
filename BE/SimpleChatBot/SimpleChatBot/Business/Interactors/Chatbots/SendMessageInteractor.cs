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

                return new ISendMessageInteractor.Response("Vui lòng đăng nhập lại", false, false);
            }

            if (!(await _encodeService.ValidateJwt(request.Jwt)))
            {

                return new ISendMessageInteractor.Response("Vui lòng đăng nhập lại", false, false);
            }

            var responseMessage = await _chatBotService.GetMessage(request.Message);
            if (responseMessage == null)
            {

                return new ISendMessageInteractor.Response("Vui lòng đăng nhập lại", true, false);
            }

            return new ISendMessageInteractor.Response(responseMessage, true, true);
        }
    }
}
