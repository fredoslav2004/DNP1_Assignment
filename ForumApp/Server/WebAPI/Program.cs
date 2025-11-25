using EfcRepositories;
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

builder.Services.AddScoped<IRepository<Post>, EfcRepository<Post>>();
builder.Services.AddScoped<IRepository<User>, EfcRepository<User>>();
builder.Services.AddScoped<IRepository<Comment>, EfcRepository<Comment>>();
builder.Services.AddDbContext<EfcRepositories.AppContext>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();