using FileSystemBackend.Endpoints;
using FileSystemBackend.Services;
using Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Add your React app's URL here
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<ProfileStorageService>();
builder.Services.AddSingleton<MatrixStorageService>();

var app = builder.Build();
app.UseHttpsRedirection();

app.UseCors();

app.MapProfileEndpoints();
app.MapMatrixEndpoints();

app.Run();


public partial class Program { }