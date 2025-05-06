using Microsoft.OpenApi.Models;
using UsersApis;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "User management for Projet Pay2Win", Description = "Documentation des Apis de gestion d'utilisateur pour le projet pay2win de panassor", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pay2Win Passsor API V1");
   });
}
app.MapGet("/", () => "Hello World!");
app.MapGet("/getUser/{id}", (int id) => UsersApi.GetUser(id));
app.MapGet("/getUsers", () => UsersApi.GetUsers());
app.MapPost("/createUser", (User user) => UsersApi.CreateUser(user));
app.MapPut("/updateUser", (User user) => UsersApi.UpdateUser(user));
app.MapDelete("/remove/{id}", (int id) => UsersApi.RemoveUser(id));

app.Run();
