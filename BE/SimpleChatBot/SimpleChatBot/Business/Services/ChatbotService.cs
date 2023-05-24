using SimpleChatBot.Business.Services.Interfaces;
using System.Net;
using System.Text;

namespace SimpleChatBot.Business.Services
{
    public class ChatbotService : IChatBotService
    {
        public async Task<string> GetMessage(string message)
        {
            string apiUrl = "https://api.openai.com/v1/completions";
            string apiKey = "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";

            // Tạo nội dung của request
            var requestData = new
            {
                model = "text-davinci-003",
                prompt = "Say this is a test",
                max_tokens = 7,
                temperature = 0
            };
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            // Tạo HttpClient
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                // Gửi request
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                // Đọc response
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
        }
    }
}
