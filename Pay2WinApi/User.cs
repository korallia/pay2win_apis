using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Security.Cryptography;

public class User
{
    public long Id { get; set; }
    public required  string Username { get; set; }
    public  required  string Email { get; set; }

    [JsonIgnore]
    public   byte[]? PasswordHash { get; set; }

    [JsonIgnore]
    public    byte[]? PasswordSalt { get; set; }

    [NotMapped]
    public string? Password { get; set; }

    public void SetPassword(string password)
    {
        PasswordHasher.Create(password, out var hash, out var salt);
        PasswordHash = hash;
        PasswordSalt = salt;
    }

    public bool VerifyPassword(string password)
    {
        return PasswordHasher.Verify(password, PasswordHash, PasswordSalt);
    }

    public void PrepareForCreate()
{
    if (string.IsNullOrWhiteSpace(Password))
        throw new ArgumentException("Le mot de passe est requis à la création.");

    SetPassword(Password);
    Password = null;
}


    private static class PasswordHasher
    {
        public static void Create(string password, out byte[] hash, out byte[] salt)
        {
            salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            hash = pbkdf2.GetBytes(32);
        }

        public static bool Verify(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, 100_000, HashAlgorithmName.SHA256);
            byte[] computedHash = pbkdf2.GetBytes(32);
            return computedHash.SequenceEqual(storedHash);
        }
    }
}
