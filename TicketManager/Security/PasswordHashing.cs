using System.Security.Cryptography;

namespace TicketManager.Security
{
    internal class PasswordHashing
    {
        // Parameter: sinnvoller Startwert (Performance abhängig vom PC)
        private const int SaltSize = 16;      // 128 bit
        private const int KeySize = 32;       // 256 bit
        private const int Iterations = 210_000;

        public static (string HashBase64, string SaltBase64) HashPassword(string password)
        {
            if (password is null)
                throw new ArgumentNullException(nameof(password));

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize);

            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }

        public static bool VerifyPassword(string password, string hashBase64, string saltBase64)
        {
            if (password is null)
                throw new ArgumentNullException(nameof(password));

            if (string.IsNullOrWhiteSpace(hashBase64) || string.IsNullOrWhiteSpace(saltBase64))
                return false;

            byte[] salt = Convert.FromBase64String(saltBase64);
            byte[] expectedHash = Convert.FromBase64String(hashBase64);

            byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                expectedHash.Length);

            // Timing-safe compare
            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
    }
}
