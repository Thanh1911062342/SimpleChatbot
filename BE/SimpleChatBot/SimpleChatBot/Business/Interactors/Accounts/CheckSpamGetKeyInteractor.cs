using SimpleChatBot.Business.Boundaries.Accounts;
using SimpleChatBot.Business.Services.Interfaces;
using SimpleChatBot.Databases.Dtos;
using SimpleChatBot.Databases.Dtos.Accounts;
using SimpleChatBot.Databases.Repositories.Interfaces;

namespace SimpleChatBot.Business.Interactors.Accounts
{
    public class CheckSpamGetKeyInteractor : ICheckSpamGetKeyInteractor
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISendMailRepository _sendMailRepository;
        private readonly IRequestService _requestService;

        public CheckSpamGetKeyInteractor(IAccountRepository accountRepository,
                                            ISendMailRepository sendMailRepository,
                                            IRequestService requestService)
        {
            _accountRepository = accountRepository;
            _sendMailRepository = sendMailRepository;
            _requestService = requestService;
        }

        public async Task<ICheckSpamGetKeyInteractor.Response> CheckSpam(ICheckSpamGetKeyInteractor.Request request)
        {
            var time = await _requestService.CalculateLimitTimeToGetKey(request.Email, request.Ip, 120);

            return new ICheckSpamGetKeyInteractor.Response(time);
        }
    }
}
