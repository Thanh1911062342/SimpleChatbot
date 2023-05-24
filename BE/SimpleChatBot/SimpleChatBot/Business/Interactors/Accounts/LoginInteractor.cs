using SimpleChatBot.Business.Boundaries.Accounts;
using SimpleChatBot.Business.Services.Interfaces;
using SimpleChatBot.Databases.Dtos;
using SimpleChatBot.Databases.Repositories.Interfaces;
using System.Globalization;

namespace SimpleChatBot.Business.Interactors.Accounts
{
    public class LoginInteractor : ILoginInteractor
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEncodeService _encodeService;

        public LoginInteractor(IAccountRepository accountRepository, IEncodeService encodeService)
        {
            _accountRepository = accountRepository;
            _encodeService = encodeService;
        }

        public async Task<ILoginInteractor.Response> Login(ILoginInteractor.Request request)
        {
            var account = await _accountRepository.GetAccountByEmail(request.LoginDto.Email);
            if (account == null)
            {
                return new ILoginInteractor.Response(new NotificationDto() { Message = "Email không chính xác", Status = false });
            }

            if (account.KeyActive != request.LoginDto.Key)
            {
                return new ILoginInteractor.Response(new NotificationDto() { Message = "Mã không chính xác, vui lòng gửi lại", Status = false });
            }

            if (account.KeyDuration.Value.Date < DateTime.Now.Date)
            {
                return new ILoginInteractor.Response(new NotificationDto() { Message = "Mã đã hết hạn, vui lòng lấy mã mới", Status = false });
            }

            var payloadItems = new Dictionary<string, string>()
            {
                { "email", request.LoginDto.Email },
                { "role", "user" },
                {"exp", DateTime.Now.Date.ToString()}
            };
            var jwt = await _encodeService.GenerateJWT(payloadItems, request.LoginDto.Key);

            if (string.IsNullOrEmpty(jwt))
            {
                return new ILoginInteractor.Response(new NotificationDto() { Message = "Lỗi không xác định, vui lòng thử lại sau", Status = false });
            }

            return new ILoginInteractor.Response(new NotificationDto() { Message = jwt, Status = true });
        }
    }
}
