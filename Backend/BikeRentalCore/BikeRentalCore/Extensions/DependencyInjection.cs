using BikeRentalCore.Clients;
using BikeRentalCore.Data;
using BikeRentalCore.Entities;
using BikeRentalCore.Security;
using BikeRentalCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalCore.Extensions;

public static class DependencyInjection
{
    public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "bikerental.db");

        services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

        services.AddScoped<DataSeeding>();

        services.AddScoped<BikeService>();
        services.AddScoped<BrandService>();
        services.AddScoped<RentalPointService>();
        services.AddScoped<UnityService>();
        services.AddScoped<TenancyService>();

        services.AddScoped<IStorageClient, S3Client>();

        services.AddSingleton<IAuthorizationHandler, InternalUserAuthorizationHandler>();

    }
}
