using System.Security.Cryptography;
using System.Text;

namespace Extensions
{
    public static class Hashing
    {
        public static string ToSHA256(this string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return GetStringFromHash(hash);
            }
        }

        public static string ToSHA512(this string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha512.ComputeHash(bytes);
                return GetStringFromHash(hash);
            }
        }

        public static string ToMD5(this string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input);
                byte[] hash = md5.ComputeHash(bytes);
                return GetStringFromHash(hash);
            }
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
