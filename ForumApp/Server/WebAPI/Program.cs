using Entities;
using FileRepositories;
using InMemoryRepositories;
using RepositoryContracts;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IRepository<Post>>(sp => new FileRepository<Post>("posts"));
builder.Services.AddScoped<IRepository<User>>(sp => new FileRepository<User>("users"));
builder.Services.AddScoped<IRepository<Comment>>(sp => new FileRepository<Comment>("comments"));

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();