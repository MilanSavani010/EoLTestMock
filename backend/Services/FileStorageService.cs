using System.Text.Json;
using Models;

namespace FileSystemBackend.Services;

public class FileStorageService
{
    private readonly string _storageDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"BACKEND");

    public FileStorageService()
    {
        // Ensure the storage directory exists
        if (!Directory.Exists(_storageDirectory))
        {
            Directory.CreateDirectory(_storageDirectory);
        }
    }

    // Get all profiles by reading all files in the directory
    public List<Profile> GetAllProfiles()
    {
        var profiles = new List<Profile>();

        foreach (var file in Directory.GetFiles(_storageDirectory, "*.json"))
        {
            var json = File.ReadAllText(file);
            var profile = JsonSerializer.Deserialize<Profile>(json);
            if (profile != null)
            {
                profiles.Add(profile);
            }
        }

        return profiles;
    }

    // Get a specific profile by ID
    public Profile? GetProfileById(string id)
    {
        var filePath = GetFilePathById(id);
        if (!File.Exists(filePath))
        {
            return null;
        }

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Profile>(json);
    }

    // Add a new profile and save it as a separate file
    public void AddProfile(Profile profile)
    {
        if(profile.Id == "")
        {
            profile.Id = Guid.NewGuid().ToString();
        }
        var filePath = GetFilePath(profile.Id, profile.Name);
        var json = JsonSerializer.Serialize(profile, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    // Update an existing profile by overwriting its file
    public void UpdateProfile(string id, Profile updatedProfile)
    {
        var filePath = GetFilePathById(id);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Profile with ID '{id}' does not exist.");
        }

        // Ensure the filename matches the updated name
        var newFilePath = GetFilePath(updatedProfile.Id, updatedProfile.Name);
        if (filePath != newFilePath)
        {
            File.Delete(filePath); // Delete the old file
        }

        var json = JsonSerializer.Serialize(updatedProfile, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(newFilePath, json);
    }

    // Delete a profile by removing its file
    public void DeleteProfile(string id)
    {
        var filePath = GetFilePathById(id);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    // Helper: Generate the file path for a profile
    private string GetFilePath(string id, string name)
    {
        var sanitizedFileName = $"{id}_{SanitizeFileName(name)}.json";
        return Path.Combine(_storageDirectory, sanitizedFileName);
    }

    // Helper: Locate a file by ID (ignores the name in the filename)
    private string GetFilePathById(string id)
    {
        return Directory.GetFiles(_storageDirectory, $"{id}_*.json").FirstOrDefault() ?? string.Empty;
    }

    // Helper: Sanitize file names to avoid invalid characters
    private string SanitizeFileName(string name)
    {
        return string.Concat(name.Split(Path.GetInvalidFileNameChars()));
    }
}
