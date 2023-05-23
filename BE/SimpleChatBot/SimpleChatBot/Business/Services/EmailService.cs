using SimpleChatBot.Business.Services.Interfaces;
using SimpleChatBot.Databases.Dtos;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SimpleChatBot.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMail(EmailDto email, int maxRetryCount)
        {
            if (IsValidEmail(email.ReceiverEmail) == false)
            {
                return;
            }

            string senderEmail = string.IsNullOrEmpty(_configuration["Email:EmailAccount"]) == true ? "" : _configuration["Email:EmailAccount"];
            string password = _configuration["Email:Password"];

            //Tạo mail
            MailMessage message = new MailMessage(senderEmail, email.ReceiverEmail);
            message.Subject = email.Subject;
            message.Body = email.Body;

            //Kết nối máy chủ SMTP
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, password);
            smtpClient.EnableSsl = true;

            int retryCount = 0;
            while (retryCount < maxRetryCount)
            {
                try
                {
                    //Tiến hành gửi mail tới SMTP Server
                    smtpClient.Send(message);
                    Console.WriteLine("Email sent successfully!");
                    break;
                }
                catch (Exception ex)
                {
                    // Trường hợp gặp lỗi khi gửi email, chúng ta sẽ hiển thị lỗi
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    retryCount++;
                    if (retryCount < maxRetryCount)
                    {
                        Console.WriteLine($"Retrying... Attempt {retryCount} of {maxRetryCount}");
                    }
                    else
                    {
                        Console.WriteLine("Maximum retry attempts exceeded.");
                    }
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
