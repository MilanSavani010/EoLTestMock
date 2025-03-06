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
builder.Services.AddSingleton<FileStorageService>();
var app = builder.Build();
var storageService = app.Services.GetRequiredService<FileStorageService>();
app.UseHttpsRedirection();

app.UseCors();
// Get all profiles
app.MapGet("/profiles", () =>
{
    var result = storageService.GetAllProfiles();
    return Results.Ok(result);
 });

// Get a specific profile
app.MapGet("/profiles/{id}", (string id) =>
{
    var profile = storageService.GetProfileById(id);
    return profile is not null ? Results.Ok(profile) : Results.NotFound();
});

// Add a new profile
app.MapPost("/profiles", (Profile profile) =>
{
    storageService.AddProfile(profile);
    return Results.Created($"/profiles/{profile.Id}", profile);
});

// Update a profile
app.MapPut("/profiles/{id}", (string id, Profile updatedProfile) =>
{
    var existingProfile = storageService.GetProfileById(id);
    if (existingProfile is null) return Results.NotFound();

    updatedProfile.Id = id; // Ensure the ID remains unchanged
    storageService.UpdateProfile(id, updatedProfile);
    return Results.NoContent();
});

// Delete a profile
app.MapDelete("/profiles/{id}", (string id) =>
{
    var existingProfile = storageService.GetProfileById(id);
    if (existingProfile is null) return Results.NotFound();

    storageService.DeleteProfile(id);
    return Results.NoContent();
});


app.Run();


public partial class Program { }