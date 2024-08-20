using Konscious.Security.Cryptography;
using System.Text;

namespace CardsServer.BLL.Infrastructure
{
    public class PasswordExtension
    {
        private const string Salt = "MI(*y37ghfu987T&(Utrg3uig3e4u";
        private const int DegreeOfParallelism = 4;
        private const int MemorySize = 8192;
        private const int Iterations = 6;

        public static string HashPassword(string password)
        {

            byte[] pass = Encoding.ASCII.GetBytes(password);
            byte[] salt = Encoding.ASCII.GetBytes(Salt);

            Argon2id argon2 = new(pass)
            {
                DegreeOfParallelism = DegreeOfParallelism,
                MemorySize = MemorySize,
                Iterations = Iterations,
                Salt = salt
            };

            byte[] hash = argon2.GetBytes(128);

            string hashBase64 = Convert.ToBase64String(hash);

            return hashBase64;
        }

        public static bool CheckPassword (string pass, string pass_reference)
        {
            string passHash = HashPassword(pass);

            if (passHash == pass_reference)
            {
                return true;
            }
            return false;
        }
    }
}
