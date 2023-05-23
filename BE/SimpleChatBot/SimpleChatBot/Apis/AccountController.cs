using Microsoft.AspNetCore.Mvc;
using SimpleChatBot.Business.Boundaries.Accounts;
using SimpleChatBot.Databases.Dtos.Accounts;

namespace SimpleChatBot.Apis
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IKeyActiveInteractor _keyActiveInteractor;
        private readonly ICheckSpamGetKeyInteractor _checkSpamGetKeyInteractor;
        private readonly ILoginInteractor _loginInteractor;

        public AccountController(IKeyActiveInteractor keyActiveInteractor, 
                                    ICheckSpamGetKeyInteractor checkSpamSendKeyInteractor,
                                    ILoginInteractor loginInteractor) {
            _keyActiveInteractor = keyActiveInteractor;
            _checkSpamGetKeyInteractor = checkSpamSendKeyInteractor;
            _loginInteractor = loginInteractor;
        }

        [HttpGet("keyActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> KeyActive(string email)
        {
            string clientIp = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            var response = await _keyActiveInteractor.KeyActive(new IKeyActiveInteractor.Request(email, clientIp));

            return Ok(response);
        }

        [HttpGet("checkSpamGetKey")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CheckSpamGetKey()
        {
            string clientIp = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            var response = await _checkSpamGetKeyInteractor.CheckSpam(new ICheckSpamGetKeyInteractor.Request("", clientIp));

            return Ok(response);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _loginInteractor.Login(new ILoginInteractor.Request(loginDto));

            return Ok(response);
        }
    }
}
