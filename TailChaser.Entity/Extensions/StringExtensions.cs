using System.Security.Cryptography;
using System.Text;

namespace TailChaser.Entity.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetHash(this string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);

            byte[] result;
            using (var sha = new SHA256Managed())
            {
                result = sha.ComputeHash(bytes);
            }
            return result;
        }
    }
}
