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
app.MapGet("/getUser/{id}", (int id) => UsersApi.GetUser(id));
app.MapGet("/getUsers", () => UsersApi.GetUsers());
app.MapPost("/createUser", (User user) => UsersApi.CreateUser(user));
app.MapPut("/updateUser", (User user) => UsersApi.UpdateUser(user));
app.MapDelete("/removeUser/{id}", (int id) => UsersApi.RemoveUser(id));

// Lancer l'application
app.Run();
