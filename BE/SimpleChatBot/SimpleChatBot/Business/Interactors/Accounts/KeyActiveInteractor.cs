using SimpleChatBot.Business.Boundaries.Accounts;
using SimpleChatBot.Business.Services.Interfaces;
using SimpleChatBot.Databases.Dtos;
using SimpleChatBot.Databases.Dtos.Accounts;
using SimpleChatBot.Databases.Entities;
using SimpleChatBot.Databases.Repositories.Interfaces;

namespace SimpleChatBot.Business.Interactors.Accounts
{
    public class KeyActiveInteractor : IKeyActiveInteractor
    {
        private readonly ISendMailRepository _sendMailRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHelperService _helperService;
        private readonly IEncodeService _encodeService;
        private readonly IRequestService _accountService;
        private readonly IEmailService _emailService;

        public KeyActiveInteractor(ISendMailRepository sendMailRepository,
                                    IAccountRepository accountRepository,
                                    IHelperService helperService,
                                    IEncodeService encodeService,
                                    IRequestService accountService,
                                    IEmailService emailService)
        {
            _sendMailRepository = sendMailRepository;
            _accountRepository = accountRepository;
            _helperService = helperService;
            _encodeService = encodeService;
            _accountService = accountService;
            _emailService = emailService;
        }

        public async Task<IKeyActiveInteractor.Response> KeyActive(IKeyActiveInteractor.Request request)
        {
            var notification = new NotificationDto();
            var litmitTime = await _accountService.CalculateLimitTimeToGetKey(request.Email, request.Ip, 120);

            if (litmitTime > 0)
            {
                notification.Message = "Limited time";
                notification.Status = false;

                return new IKeyActiveInteractor.Response(notification, litmitTime);
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                notification.Message = "Email is not valid";
                notification.Status = false;

                return new IKeyActiveInteractor.Response(notification);
            }

            if (await _accountRepository.GetAccountByEmail(request.Email) == null)
            {
                Account account = new Account()
                {
                    Email = request.Email,
                    Ip = request.Ip,
                };

                _helperService.TrimStringProperties(account);

                await _accountRepository.Create(account);
            }
            var key = await _encodeService.EncodeToBase64(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (key == null)
            {
                notification.Message = "Unkown Error";
                notification.Status = false;

                return new IKeyActiveInteractor.Response(notification);
            }

            var accountToUpdateKey = await _accountRepository.GetAccountByEmail(request.Email);
            accountToUpdateKey.KeyActive = key;
            accountToUpdateKey.KeyDuration = DateTime.Now;
            await _accountRepository.Update(accountToUpdateKey);

            var email = new EmailDto()
            {
                ReceiverEmail = request.Email,
                Subject = "Mã kích hoạt tài khoản",
                Body = $"Mã ====> [{key}]",
            };
            Task.Run(() => _emailService.SendMail(email, 1));

            var sendMail = new SendMail()
            {
                AccountId = accountToUpdateKey.Id,
                MessageContent = email.Body,
                Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
                Ip = accountToUpdateKey.Ip
            };
            await _sendMailRepository.Create(sendMail);

            notification.Message = "Success";
            notification.Status = true;
            

            return new IKeyActiveInteractor.Response(notification, 120);
        }
    }
}
