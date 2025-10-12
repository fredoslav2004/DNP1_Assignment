using Entities;
using FileRepositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IRepository<Post>, FileRepository<Post>>();
builder.Services.AddScoped<IRepository<User>, FileRepository<User>>();
builder.Services.AddScoped<IRepository<Comment>, FileRepository<Comment>>();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();