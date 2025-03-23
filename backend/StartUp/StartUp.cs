using FileSystemBackend.Endpoints;
using FileSystemBackend.Services;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Register services
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173") // Add your React app's URL here
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        services.AddSingleton<IProfileStorageService, ProfileStorageService>();
        services.AddSingleton<IMatrixStorageService, MatrixStorageService>();
        services.AddSingleton<IEoLApplicationService, MockEoLApplicationService>();
    }

    // Configure the middleware and endpoints
    public void Configure(WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseCors();

        app.MapProfileEndpoints();
        app.MapMatrixEndpoints();
        app.MapEoLTestEndpoints();
    }
}
