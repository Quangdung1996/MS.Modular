using System;
using System.Security.Cryptography;
using System.Text;

namespace MS.Modular.BuildingBlocks.Domain.Extenstions
{
    public static class AccountExetions
    {
        public static string ToLower(string text)
        {
            return text.ToLower();
        }

        public static string HashPassword(string password, string hashSalt)
        {
            using var md5 = MD5.Create();
            var saltAndPassword = String.Concat(password, hashSalt);
            var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        public static string GeneraSalt()
        {
            byte[] bytes = new byte[15];
            using var keyGenerator = RandomNumberGenerator.Create();
            keyGenerator.GetBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}