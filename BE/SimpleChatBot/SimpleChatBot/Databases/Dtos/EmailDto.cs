namespace SimpleChatBot.Databases.Dtos
{
    public class EmailDto
    {
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attach { get; set; }
    }
}
