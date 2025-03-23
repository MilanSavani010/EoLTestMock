using FileSystemBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace FileSystemBackend.Endpoints;

public static class ProfileEndpoints
{
    public static void MapProfileEndpoints(this WebApplication app)
    {

        // Get all profiles
        app.MapGet("/profiles", ([FromServices]IProfileStorageService storageService) =>
        {
            var result = storageService.GetAllProfiles();
            return Results.Ok(result);
        
        });

        // Get a specific profile
        app.MapGet("/profiles/{id}", (string id, [FromServices] IProfileStorageService storageService) =>
        {
            var profile = storageService.GetProfileById(id);
            return profile is not null ? Results.Ok(profile) : Results.NotFound();
        });

        // Add a new profile
        app.MapPost("/profiles", (Profile profile, [FromServices] IProfileStorageService storageService) =>
        {
            storageService.AddProfile(profile);
            return Results.Created($"/profiles/{profile.Id}", profile);
        });

        // Update a profile
        app.MapPut("/profiles/{id}", (string id, Profile updatedProfile, [FromServices] IProfileStorageService storageService) =>
        {
            var existingProfile = storageService.GetProfileById(id);
            if (existingProfile is null) return Results.NotFound();

            updatedProfile.Id = id; // Ensure the ID remains unchanged
            storageService.UpdateProfile(id, updatedProfile);
            return Results.NoContent();
        });

        // Delete a profile
        app.MapDelete("/profiles/{id}", (string id, [FromServices] IProfileStorageService storageService) =>
        {
            var existingProfile = storageService.GetProfileById(id);
            if (existingProfile is null) return Results.NotFound();

            storageService.DeleteProfile(id);
            return Results.NoContent();
        });
    }
}