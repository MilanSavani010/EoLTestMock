using FileSystemBackend.Services;
using Models;

namespace FileSystemBackend.Endpoints;

public static class MatrixEndpoints
{
    public static void MapMatrixEndpoints(this WebApplication app)
    {
        var storageService = app.Services.GetRequiredService<MatrixStorageService>();

        // Get all matrices
        app.MapGet("/matrices", () =>
        {
            var result = storageService.GetAllMatrices();
            return Results.Ok(result);
        });

        // Get a specific matrix
        app.MapGet("/matrices/{id}", (string id) =>
        {
            var matrix = storageService.GetMatrixById(id);
            return matrix is not null ? Results.Ok(matrix) : Results.NotFound();
        });

        // Add a new matrix
        app.MapPost("/matrices", (Matrix matrix) =>
        {
            storageService.AddMatrix(matrix);
            return Results.Created($"/matrices/{matrix.Id}", matrix);
        });

        // Update a matrix
        app.MapPut("/matrices/{id}", (string id, Matrix updatedMatrix) =>
        {
            var existingMatrix = storageService.GetMatrixById(id);
            if (existingMatrix is null) return Results.NotFound();

            updatedMatrix.Id = id; // Ensure the ID remains unchanged
            storageService.UpdateMatrix(id, updatedMatrix);
            return Results.NoContent();
        });

        // Delete a matrix
        app.MapDelete("/matrices/{id}", (string id) =>
        {
            var existingMatrix = storageService.GetMatrixById(id);
            if (existingMatrix is null) return Results.NotFound();

            storageService.DeleteMatrix(id);
            return Results.NoContent();
        });
    }
}