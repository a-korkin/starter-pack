using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using System.Reflection;
using Application.Common.Interfaces;
using Infrastructure.Services;
using Domain.Entities.Admin;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<ApplicationDbContext>
            (
                opt => opt.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                )
            );
            // services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IAuthService, AuthService>();

            // repositories
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();

            return services;
        }
    }
}