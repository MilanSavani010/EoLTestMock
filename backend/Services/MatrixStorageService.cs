using System.Text.Json;
using Models;
namespace FileSystemBackend.Services;

public class MatrixStorageService
{
    static string _rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BACKEND");
    static string _matrixStorageDirectory = Path.Combine(_rootDirectory, "matrices");

    public MatrixStorageService()
    {
        // Ensure the storage directory exists
        if (!Directory.Exists(_rootDirectory))
        {
            Directory.CreateDirectory(_rootDirectory);
        }
        if (!Directory.Exists(_matrixStorageDirectory))
        {
            Directory.CreateDirectory(_matrixStorageDirectory);
        }
    }

    // Get all matrices by reading all files in the directory
    public List<Matrix> GetAllMatrices()
    {
        var matrices = new List<Matrix>();
        foreach (var file in Directory.GetFiles(_matrixStorageDirectory, "*.json"))
        {
            var json = File.ReadAllText(file);
            var matrix = JsonSerializer.Deserialize<Matrix>(json);
            if (matrix != null)
            {
                matrices.Add(matrix);
            }
        }
        return matrices;
    }

    // Get a specific matrix by ID
    public Matrix? GetMatrixById(string id)
    {
        var filePath = GetFilePathById(id);
        if (!File.Exists(filePath))
        {
            return null;
        }
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Matrix>(json);
    }

    // Add a new matrix and save it as a separate file
    public void AddMatrix(Matrix matrix)
    {
        if (string.IsNullOrEmpty(matrix.Id))
        {
            matrix.Id = Guid.NewGuid().ToString();
        }
        var filePath = GetFilePath(matrix.Id, matrix.Name);
        var json = JsonSerializer.Serialize(matrix, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    // Update an existing matrix by overwriting its file
    public void UpdateMatrix(string id, Matrix updatedMatrix)
    {
        var filePath = GetFilePathById(id);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Matrix with ID '{id}' does not exist.");
        }

        // Ensure the filename matches the updated name
        var newFilePath = GetFilePath(updatedMatrix.Id, updatedMatrix.Name);
        if (filePath != newFilePath)
        {
            File.Delete(filePath); // Delete the old file
        }

        var json = JsonSerializer.Serialize(updatedMatrix, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(newFilePath, json);
    }

    // Delete a matrix by removing its file
    public void DeleteMatrix(string id)
    {
        var filePath = GetFilePathById(id);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    // Helper: Generate the file path for a matrix
    private string GetFilePath(string id, string name)
    {
        var sanitizedFileName = $"{id}_{SanitizeFileName(name)}.json";
        return Path.Combine(_matrixStorageDirectory, sanitizedFileName);
    }

    // Helper: Locate a file by ID (ignores the name in the filename)
    private string GetFilePathById(string id)
    {
        return Directory.GetFiles(_matrixStorageDirectory, $"{id}_*.json").FirstOrDefault() ?? string.Empty;
    }

    // Helper: Sanitize file names to avoid invalid characters
    private string SanitizeFileName(string name)
    {
        return string.Concat(name.Split(Path.GetInvalidFileNameChars()));
    }
}