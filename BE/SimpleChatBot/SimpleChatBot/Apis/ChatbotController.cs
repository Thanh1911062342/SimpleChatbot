using Microsoft.AspNetCore.Mvc;
using SimpleChatBot.Business.Boundaries.Chatbots;
using System.Text;

namespace SimpleChatBot.Apis
{
    [ApiController]
    [Route("api/chatbot")]
    public class ChatbotController : Controller
    {
        private readonly ISendMessageInteractor _sendMessageInteractor;

        public ChatbotController(ISendMessageInteractor sendMessageInteractor) 
        {
            _sendMessageInteractor = sendMessageInteractor;
        }

        [HttpPost("sendMessage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendMessage()
        {
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("\"", "");
            string message = "";
            using (StreamReader reader = new StreamReader(Request.Body,Encoding.UTF8))
            {
                message = await reader.ReadToEndAsync();

                var response = await _sendMessageInteractor.SendMessage(new ISendMessageInteractor.Request(jwt, message));

                return Ok(response);
            }
        }
    }
}
