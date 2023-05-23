using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleChatBot.Databases.Entities
{
    public class SendMail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public string? MessageContent { get; set; }

        public string? Ip { get; set; }

        public long? Timestamp { get; set; }
    }
}
