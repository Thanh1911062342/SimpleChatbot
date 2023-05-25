using Newtonsoft.Json;
using SimpleChatBot.Business.Services.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;

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
                prompt = message,
                max_tokens = 1000,
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

                // Parse the JSON string into a JsonDocument
                var doc = JsonDocument.Parse(responseContent);

                // Get the root element of the JSON document
                var root = doc.RootElement;

                // Access the "choices" property of the root object. 
                // This returns a JsonElement which represents the "choices" array.
                var choices = root.GetProperty("choices");

                // Access the first element in the "choices" array. 
                // This returns a JsonElement which represents the first object in the "choices" array.
                var firstChoice = choices[0];

                // Access the "text" property of the first choice object and get its string value. 
                // This returns the value of the "text" property as a string.
                var text = firstChoice.GetProperty("text").GetString();

                await Task.Delay(5000);

                return text;
            }
        }
    }
}
