using SimpleChatBot.Business.Services.Interfaces;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace SimpleChatBot.Business.Services
{
    public class EncodeService : IEncodeService
    {
        public async Task<string> EncodeToBase64(string toEncode)
        {
            byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(toEncode);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue.Length >= 15 ? returnValue[..^2] : returnValue;
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
    }
}
