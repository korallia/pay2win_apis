public class UserViewModel
{
    public long Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }

    public static UserViewModel FromUser(User user)
    {
        return new UserViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }
}
