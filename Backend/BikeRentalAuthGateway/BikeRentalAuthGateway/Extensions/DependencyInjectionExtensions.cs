using BikeRentalAuthGateway.Data;
using BikeRentalAuthGateway.Helpers;
using BikeRentalAuthGateway.Middlewares;
using BikeRentalAuthGateway.Services;
using BikeRentalAuthGateway.UseCases;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalAuthGateway.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "bikerentals-auth.db");

        services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));


        //services.AddDbContext<AppDbContext>(options => 
        //    options.UseNpgsql(
        //        Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
        //        ?? configuration.GetConnectionString("DefaultConnection")
        //    )
        //);

        services.AddScoped<SignInUseCase>();
        services.AddScoped<SignUpUseCase>();
        services.AddScoped<GetUserUseCase>();
        services.AddScoped<JsonWebTokenHelper>();
        services.AddScoped<TokenService>();
        services.AddScoped<UserService>();
        services.AddScoped<LogService>();

        services.AddTransient<GlobalExceptionHandler>();
        services.AddTransient<RequestForwardingMiddleware>();
    }
}
