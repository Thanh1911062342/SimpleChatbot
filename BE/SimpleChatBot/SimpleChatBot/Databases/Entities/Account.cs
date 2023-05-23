using System;
using System.Collections.Generic;

namespace SimpleChatBot.Databases.Entities
{
    public partial class Account
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? KeyActive { get; set; }
        public string? Ip { get; set; }
        public DateTime? KeyDuration { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<SendMail> SendMails { get; set; }
    }
}
