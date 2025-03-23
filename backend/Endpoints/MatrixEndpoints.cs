using Microsoft.AspNetCore.Mvc;
using Models;

namespace FileSystemBackend.Endpoints;

public static class MatrixEndpoints
{
    public static void MapMatrixEndpoints(this WebApplication app)
    {

        // Get all matrices
        app.MapGet("/matrices", ([FromServices] IMatrixStorageService storageService) =>
        {
            var result = storageService.GetAllMatrices();
            return Results.Ok(result);
        });

        // Get a specific matrix
        app.MapGet("/matrices/{id}", (string id, [FromServices] IMatrixStorageService storageService) =>
        {
            var matrix = storageService.GetMatrixById(id);
            return matrix is not null ? Results.Ok(matrix) : Results.NotFound();
        });

        // Add a new matrix
        app.MapPost("/matrices", (Matrix matrix, [FromServices] IMatrixStorageService storageService) =>
        {
            storageService.AddMatrix(matrix);
            return Results.Created($"/matrices/{matrix.Id}", matrix);
        });

        // Update a matrix
        app.MapPut("/matrices/{id}", (string id, Matrix updatedMatrix, [FromServices] IMatrixStorageService storageService) =>
        {
            var existingMatrix = storageService.GetMatrixById(id);
            if (existingMatrix is null) return Results.NotFound();

            updatedMatrix.Id = id; // Ensure the ID remains unchanged
            storageService.UpdateMatrix(id, updatedMatrix);
            return Results.NoContent();
        });

        // Delete a matrix
        app.MapDelete("/matrices/{id}", (string id, [FromServices] IMatrixStorageService storageService) =>
        {
            var existingMatrix = storageService.GetMatrixById(id);
            if (existingMatrix is null) return Results.NotFound();

            storageService.DeleteMatrix(id);
            return Results.NoContent();
        });
    }
}