using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajouter la configuration de la base de données SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Ajout Configuration Services
builder.Services.AddScoped<UserService>();



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






//Ajout Endpoints
app.MapUserEndpoints();

// Lancer l'application
app.Run();
