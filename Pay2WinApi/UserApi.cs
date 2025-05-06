namespace UsersApis; 



 public class UsersApi
 {
   private static List<User> _users = new List<User>()
   {
    new User{ Id=1, Username="test1", Email= "test1@gmail.com", Password= "password1test" },
    new User{ Id=2, Username="test2", Email= "test2@gmail.com", Password= "password2test" },
    new User{ Id=3, Username="test3", Email= "test3@gmail.com", Password= "password3test" },
   };

   public static List<User> GetUsers(AppDbContext dbContext)
{
    return dbContext.Users.ToList();  // Remplace _users par dbContext.Users
}


   public static User ? GetUser(int id,AppDbContext dbContext) 
   {
      return dbContext.Users.FirstOrDefault(user => user.Id == id); // Récupère l'utilisateur avec cet ID
   } 

   public static User CreateUser(User user) 
   {
     _users.Add(user);
     return user;
   }

   public static User UpdateUser(User update) 
   {
     _users = _users.Select(user =>
     {
       if (user.Id == update.Id)
       {
         user.Username = update.Username;
         user.Email = update.Email;
         user.Password = update.Password;
       }
       return user;
     }).ToList();
    return _users.FirstOrDefault(u => u.Id == update.Id);
   }

    public static void RemoveUser(AppDbContext dbContext, int id)
{
    var user = dbContext.Users.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
        Console.WriteLine($"Utilisateur avec ID {id} non trouvé.");
        return; // L'utilisateur n'existe pas
    }

    dbContext.Users.Remove(user); // Supprime l'utilisateur
    dbContext.SaveChanges(); // Sauvegarde les changements
    Console.WriteLine($"Utilisateur avec ID {id} supprimé avec succès.");
}

 }