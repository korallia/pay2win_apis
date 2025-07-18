using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/getUsers", (UserService service) =>
        {
            var users = service.GetUsers();
            return users.Any() ? Results.Ok(users) : Results.NotFound("Aucun utilisateur trouvé.");
        });

        app.MapGet("/getUser/{id}", (UserService service, int id) =>
        {
            var user = service.GetUser(id);
            return user is null ? Results.NotFound() : Results.Ok(user);
        });

        app.MapPost("/login", (UserService service, LoginModel model) =>
        {
            var user = service.GetUserByUsername(model.Username);
            if (user == null)
                return Results.Unauthorized();

            bool valid = user.VerifyPassword(model.Password);

            if (!valid)
                return Results.Unauthorized();

            return Results.Ok(UserViewModel.FromUser(user));
        });


        app.MapPost("/createUser", (UserService service, UserModel model) =>
        {
            var newUser = model.ToUser();
            var createdUser = service.CreateUser(newUser);
            return Results.Ok(UserViewModel.FromUser(createdUser));
        });

        app.MapPut("/updateUser", (UserService service, UserModel model) =>
        {
            var user = service.GetUser(model.Id);
            if (user is null)
                return Results.NotFound();

            model.ApplyToUser(user);
            service.UpdateUser(user);

            return Results.Ok(UserViewModel.FromUser(user));
        });


        app.MapDelete("/deleteUser/{id}", (UserService service, int id) =>
        {
            var success = service.RemoveUser(id);
            return success
                ? Results.Ok(new { message = $"Utilisateur ID {id} supprimé." })
                : Results.NotFound(new { message = $"Utilisateur ID {id} non trouvé." });
        });
        
        
    }
}
