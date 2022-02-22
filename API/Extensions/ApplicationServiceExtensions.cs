using API.Data;
using API.Data.Repositories;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
      public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
      {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICongregationRepository, CongregationRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<ILiteratureRepository, LiteratureRepository>();
        services.AddScoped<IRequestRepository, RequestRepository>();
        services.AddScoped<LogUserActivity>();
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        services.AddDbContext<DataContext>(opt =>
        {
          opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
          opt.EnableSensitiveDataLogging();
        });

        return services;
      }
    }
}