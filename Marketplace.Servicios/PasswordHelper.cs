using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Marketplace.Servicios
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            HashAlgorithm hash = new SHA256Managed();
            byte[] bytesTextoPlano = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] bytesHash = hash.ComputeHash(bytesTextoPlano);
            return System.Convert.ToBase64String(bytesHash);
        }

        public static bool VerificarPassword(string password, string hashedPassword)
        {
            string auxPassword = HashPassword(password);
            return auxPassword == hashedPassword;
        }
    }
}