 
 [Serializable]
public class User
{
    public long Id { get; set; }  // bigint = long en C#
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}