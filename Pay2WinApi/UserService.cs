using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;



public class UserService
{
    private readonly AppDbContext _dbContext;
    

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<User> GetUsers()
    {
        return _dbContext.Users.ToList();
    }

    public User? GetUser(long id)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == id);
    }

    public User CreateUser(User newUser)
    {
        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
        return newUser;
    }

    public User? UpdateUser(User updatedUser)
    {
        var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
        if (existingUser == null) return null;

        existingUser.Username = updatedUser.Username;
        existingUser.Email = updatedUser.Email;
        existingUser.Password = updatedUser.Password;

        _dbContext.SaveChanges();
        return existingUser;
    }

    public bool RemoveUser(int id)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return false;

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
        return true;
    }
}
