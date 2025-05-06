namespace UsersApis; 



 public class UsersApi
 {
   private static List<User> _users = new List<User>()
   {
    new User{ Id=1, Username="test1", Email= "test1@gmail.com", PasswordHash= "password1test" },
    new User{ Id=2, Username="test2", Email= "test2@gmail.com", PasswordHash= "password2test" },
    new User{ Id=3, Username="test3", Email= "test3@gmail.com", PasswordHash= "password3test" },
   };

   public static List<User> GetUsers() 
   {
     return _users;
   } 

   public static User ? GetUser(int id) 
   {
     return _users.SingleOrDefault(user => user.Id == id);
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
         user.PasswordHash = update.PasswordHash;
       }
       return user;
     }).ToList();
    return _users.FirstOrDefault(u => u.Id == update.Id);
   }

   public static void RemoveUser(int removeid)
   {
     _users = _users.FindAll(user => user.Id != removeid).ToList();
   }
 }