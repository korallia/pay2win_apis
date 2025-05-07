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

        app.MapPost("/createUser", (UserService service, User newUser) =>
        {
            var createdUser = service.CreateUser(newUser);
            return Results.Ok(createdUser);
        });

        app.MapPut("/updateUser", (UserService service, User updatedUser) =>
        {
            var user = service.UpdateUser(updatedUser);
            return user is null
                ? Results.NotFound(new { message = $"Utilisateur ID {updatedUser.Id} non trouvé." })
                : Results.Ok(user);
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
