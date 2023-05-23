namespace SimpleChatBot.Business.Services.Interfaces
{
    public interface IEncodeService
    {
        public Task<string> EncodeToBase64(string toEncode);

        public Task<string> GenerateJWT(Dictionary<string, string> payloadItems, string key);
    }
}
