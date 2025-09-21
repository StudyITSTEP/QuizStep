using System.Security.Cryptography;

namespace QuizStep.Infrastructure.Services;

public static class TokenHasher
{
    public static string GenerateToken(int size = 16)
    {
        var randomNumber = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber);
    }

    public static string HashToken(string token, string salt)
    {
        using var sha256 = SHA256.Create();
        
        var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
        var saltBytes = System.Text.Encoding.UTF8.GetBytes(salt);
        var tokenWithSalt = new byte[tokenBytes.Length + salt.Length];;
        
        Buffer.BlockCopy(tokenBytes, 0, tokenWithSalt, 0, tokenBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, tokenWithSalt, tokenBytes.Length, saltBytes.Length);

        var hash = sha256.ComputeHash(tokenWithSalt);
        return Convert.ToBase64String(hash);
    }

    public static bool VerifyToken(string token, string salt, string hash)
    {
        var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
        var saltBytes = System.Text.Encoding.UTF8.GetBytes(salt);
        
        var hashToCheckBytes = System.Text.Encoding.UTF8.GetBytes(HashToken(token, salt));
        var expectedHash = System.Text.Encoding.UTF8.GetBytes(hash);
        
        return CryptographicOperations.FixedTimeEquals(hashToCheckBytes, expectedHash);
    }
}