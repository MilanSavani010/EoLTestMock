using FileSystemBackend.Services;
using Models;

namespace FileSystemBackend.Endpoints;

public static class ProfileEndpoints
{
    public static void MapProfileEndpoints(this WebApplication app)
    {
        var storageService = app.Services.GetRequiredService<ProfileStorageService>();

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
    }
}