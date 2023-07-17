using BCryptNet = BCrypt.Net.BCrypt;

namespace OnlineCoffeeShop.Core.Utilities
{
    public static class PasswordHashAndVerification
    {
        public static string HashPassword(string password)
        {
            string salt = BCryptNet.GenerateSalt();
            string hashedPassword = BCryptNet.HashPassword(password, salt);
            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCryptNet.Verify(password, hashedPassword);
        }
    }
}
