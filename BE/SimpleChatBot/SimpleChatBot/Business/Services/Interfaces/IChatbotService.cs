namespace SimpleChatBot.Business.Services.Interfaces
{
    public interface IChatBotService
    {
        public Task<string> GetMessage(string message);
    }
}
