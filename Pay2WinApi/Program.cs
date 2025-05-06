using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UsersApis;

var builder = WebApplication.CreateBuilder(args);

// Ajouter la configuration de la base de données SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter la configuration Swagger pour la documentation de l'API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User management for Projet Pay2Win",
        Description = "Documentation des Apis de gestion d'utilisateur pour le projet pay2win de panassor",
        Version = "v1"
    });
});

var app = builder.Build();

// Utiliser Swagger en mode développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pay2Win Passsor API V1");
    });
}

// Ajouter les routes de l'API
app.MapGet("/", () => "Hello World!");
app.MapGet("/getUser/{id}", (AppDbContext dbContext, int id) => 
{
    var user = UsersApi.GetUser(id, dbContext);
    if (user == null)
        return Results.NotFound(); // Si l'utilisateur n'existe pas
    return Results.Ok(user); // Sinon retourne l'utilisateur trouvé
});

app.MapGet("/getUsers", (AppDbContext dbContext) =>
{
    var users = UsersApi.GetUsers(dbContext);  // Appelle la fonction GetUsers avec dbContext
    if (users == null || !users.Any())
    {
        return Results.NotFound("Aucun utilisateur trouvé.");
    }
    return Results.Ok(users);  // Retourne les utilisateurs
});

app.MapPost("/createUser", (User user) => UsersApi.CreateUser(user));
app.MapPut("/updateUser", (User user) => UsersApi.UpdateUser(user));
app.MapDelete("/deleteUser/{removeId}", (AppDbContext dbContext, int removeId) =>
{
    Console.WriteLine($"Tentative de suppression de l'utilisateur {removeId}");

    var user = dbContext.Users.FirstOrDefault(user => user.Id == removeId);

    if (user == null)
    {
        Console.WriteLine($"Utilisateur {removeId} non trouvé");
        return Results.NotFound(new { message = $"Utilisateur avec ID {removeId} non trouvé." });
    }

    UsersApi.RemoveUser(dbContext, removeId);

    return Results.Ok(new { message = $"Utilisateur avec ID {removeId} supprimé avec succès." });
});




// Lancer l'application
app.Run();
