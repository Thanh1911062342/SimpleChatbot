using SimpleChatBot.Business.Services.Interfaces;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;
using SimpleChatBot.Databases.Repositories.Interfaces;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace SimpleChatBot.Business.Services
{
    public class EncodeService : IEncodeService
    {
        private readonly IAccountRepository _accountRepository;

        public EncodeService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<string> EncodeToBase64(string toEncode)
        {
            byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(toEncode);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public async Task<string> DecodeFromBase64(string base64String)
        {
            byte[] data = Convert.FromBase64String(base64String);
            string decodedString = Encoding.UTF8.GetString(data);
            return decodedString;
        }

        public async Task<string> GenerateJWT(Dictionary<string, string> payloadItems, string key)
        {
            // Định nghĩa header của JWT. Header chứa loại token (JWT) và thuật toán được sử dụng (HS256).
            var header = new { alg = "HS256", typ = "JWT" };

            // Chuyển đổi header thành JSON, sau đó mã hóa chúng bằng base64.
            var headerJson = JsonConvert.SerializeObject(header);
            var headerBytes = Encoding.UTF8.GetBytes(headerJson);
            var headerBase64 = Convert.ToBase64String(headerBytes);

            // Chuyển đổi payload thành JSON, sau đó mã hóa chúng bằng base64.
            var payloadJson = JsonConvert.SerializeObject(payloadItems);
            var payloadBytes = Encoding.UTF8.GetBytes(payloadJson);
            var payloadBase64 = Convert.ToBase64String(payloadBytes);

            // Kết hợp header và payload lại với nhau để tạo phần đầu của JWT.
            var toBeSigned = $"{headerBase64}.{payloadBase64}";

            // Tạo chữ ký cho JWT bằng cách sử dụng thuật toán HMAC với key được cung cấp.
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var signature = HashHmac(toBeSigned, keyBytes);
            var signatureBase64 = Convert.ToBase64String(signature);

            // Kết hợp tất cả các phần lại với nhau để tạo JWT hoàn chỉnh.
            var jwt = $"{toBeSigned}.{signatureBase64}";
            return jwt;
        }

        // Hàm để tạo chữ ký cho JWT bằng cách sử dụng thuật toán HMAC với key được cung cấp.
        private byte[] HashHmac(string message, byte[] key)
        {
            using var hash = new HMACSHA256(key);
            var bytes = Encoding.UTF8.GetBytes(message);
            var hashedMessage = hash.ComputeHash(bytes);
            return hashedMessage;
        }


        public async Task<bool> ValidateJwt(string jwt)
        {
            var parts = jwt.Split('.');
            if (parts.Length != 3)
            {
                return false;
            }

            var header = parts[0];
            var payload = parts[1];
            var signature = parts[2];

            var payloadJson = await DecodeFromBase64(payload);
            var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(payloadJson);
            string email = jsonObject["email"].ToString();

            string key = await _accountRepository.GetKeyByEmail(email);
            if (key == null)
            {
                return false;
            }

            var bytesToSign = Encoding.UTF8.GetBytes(header + "." + payload);
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                var computedSignature = hmac.ComputeHash(bytesToSign);
                var computedSignatureBase64Url = Base64UrlEncode(computedSignature);
                if (computedSignatureBase64Url != signature)
                {
                    return false;
                }
            }

            // Base64Url decode payload
            var jsonPayload = Encoding.UTF8.GetString(Base64UrlDecode(payload));

            // Parse payload
            var claims = JObject.Parse(jsonPayload);

            var exp = claims["exp"]?.Value<string>();
            DateTime date;
            bool isValidDate = DateTime.TryParse(exp, out date);
            if (!isValidDate)
            {
                return false;
            }
            else
            {
                int comparisonResult = DateTime.Compare(date, DateTime.Now.Date);
                if (comparisonResult < 0)
                {
                    return false;
                }
            }

            return true;
        }

        private byte[] Base64UrlDecode(string base64Url)
        {
            var base64 = base64Url.Replace('_', '/').Replace('-', '+');
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private string Base64UrlEncode(byte[] bytes)
        {
            var base64 = Convert.ToBase64String(bytes);
            return base64.Replace('+', '-').Replace('/', '_');
        }
    }
}
