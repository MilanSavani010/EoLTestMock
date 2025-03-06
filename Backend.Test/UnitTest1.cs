using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Models;

namespace FileSystemBackend.Tests;

public class ProfileEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProfileEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_All_Profiles_Should_Return_Empty_List_Initially()
    {
        // Act
        var response = await _client.GetAsync("/profiles");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var profiles = await response.Content.ReadFromJsonAsync<List<Profile>>();
        profiles.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Add_Profile_Should_Create_Profile()
    {
        // Arrange
        var newProfile = new Profile
        {
            Name = "Test Profile",
            Matrices = new List<Matrix>
            {
                new Matrix { Name = "Matrix A" }
            },
            Sequences = new List<Sequence>
            {
                new Sequence { Name = "Sequence A" }
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/profiles", newProfile);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Verify profile was added
        var profiles = await _client.GetFromJsonAsync<List<Profile>>("/profiles");
        profiles.Should().Contain(p => p.Name == "Test Profile");
    }

    [Fact]
    public async Task Get_Profile_By_Id_Should_Return_Profile()
    {
        // Arrange
        var newProfile = new Profile
        {
            Name = "Get Test Profile"
        };
        var createResponse = await _client.PostAsJsonAsync("/profiles", newProfile);
        var createdProfile = await createResponse.Content.ReadFromJsonAsync<Profile>();

        // Act
        var response = await _client.GetAsync($"/profiles/{createdProfile!.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var profile = await response.Content.ReadFromJsonAsync<Profile>();
        profile.Should().NotBeNull();
        profile!.Name.Should().Be("Get Test Profile");
    }

    [Fact]
    public async Task Update_Profile_Should_Modify_Profile()
    {
        // Arrange
        var newProfile = new Profile
        {
            Name = "Update Test Profile"
        };
        var createResponse = await _client.PostAsJsonAsync("/profiles", newProfile);
        var createdProfile = await createResponse.Content.ReadFromJsonAsync<Profile>();
        createdProfile!.Name = "Updated Profile Name";

        // Act
        var updateResponse = await _client.PutAsJsonAsync($"/profiles/{createdProfile.Id}", createdProfile);

        // Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Verify the profile was updated
        var updatedProfile = await _client.GetFromJsonAsync<Profile>($"/profiles/{createdProfile.Id}");
        updatedProfile.Should().NotBeNull();
        updatedProfile!.Name.Should().Be("Updated Profile Name");
    }

    [Fact]
    public async Task Delete_Profile_Should_Remove_Profile()
    {
        // Arrange
        var newProfile = new Profile
        {
            Name = "Delete Test Profile"
        };
        var createResponse = await _client.PostAsJsonAsync("/profiles", newProfile);
        var createdProfile = await createResponse.Content.ReadFromJsonAsync<Profile>();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/profiles/{createdProfile!.Id}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Verify the profile was deleted
        var getResponse = await _client.GetAsync($"/profiles/{createdProfile.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
