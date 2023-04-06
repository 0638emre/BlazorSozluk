using System.Security.Cryptography;
using System.Text;

namespace BlazorSozluk.Common.Infrastructure
{
    public class PasswordEncryptor
    {
        public static string Encrypt(string password)
        {
            using var md5 = MD5.Create();

            byte[] intputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(intputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
