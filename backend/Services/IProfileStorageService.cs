using Models;

public interface IProfileStorageService
{
    List<Profile> GetAllProfiles();
    Profile? GetProfileById(string id);
    void AddProfile(Profile profile);
    void UpdateProfile(string id,Profile updatedProfile);
    void DeleteProfile(string id);

}