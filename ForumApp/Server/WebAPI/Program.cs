using Entities;
using FileRepositories;
using InMemoryRepositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IRepository<Post>, InMemoryRepository<Post>>();
builder.Services.AddScoped<IRepository<User>, InMemoryRepository<User>>();
builder.Services.AddScoped<IRepository<Comment>, InMemoryRepository<Comment>>();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();