public class UserModel
{

    public long Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    // Création complète
    public User ToUser()
    {
        if (string.IsNullOrWhiteSpace(Username) || 
            string.IsNullOrWhiteSpace(Email) || 
            string.IsNullOrWhiteSpace(Password))
        {
            throw new ArgumentException("Tous les champs sont requis pour la création.");
        }

        var user = new User
        {
            Username = Username,
            Email = Email,
            Password = Password
        };

        user.PrepareForCreate();
        return user;
    }

    // Mise à jour partielle
    public void ApplyToUser(User user)
    {
        if (!string.IsNullOrWhiteSpace(Username))
            user.Username = Username;

        if (!string.IsNullOrWhiteSpace(Email))
            user.Email = Email;

        if (!string.IsNullOrWhiteSpace(Password))
        {
            user.SetPassword(Password);
        }
    }
}
