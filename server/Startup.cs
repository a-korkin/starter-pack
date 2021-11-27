using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using server.Authorization;
using server.DbContexts;
using server.Services;
using server.Repositories;

namespace server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => 
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:AccessKey"]))
                    };
                });

            services.AddTransient<IAuthorizationHandler, UserClaimsHandler>();

            services.AddAuthorization(opts => {
                opts.AddPolicy("GenderLimit",
                    policy => policy.Requirements.Add(new UserClaimsRequirement()));
            });

            services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // repos for admin scheme
            services.AddScoped<IBaseRepository<Entities.Admin.EntityType>, BaseRepository<Entities.Admin.EntityType>>();
            services.AddScoped<IBaseRepository<Entities.Admin.User>, BaseRepository<Entities.Admin.User>>();
            services.AddScoped<IBaseRepository<Entities.Admin.Claim>, BaseRepository<Entities.Admin.Claim>>();
            services.AddScoped<IBaseRepository<Entities.Admin.Role>, BaseRepository<Entities.Admin.Role>>();

            // repos for common scheme
            services.AddScoped<IBaseRepository<Entities.Common.Entity>, BaseRepository<Entities.Common.Entity>>();
            services.AddScoped<IBaseRepository<Entities.Common.Person>, BaseRepository<Entities.Common.Person>>();

            // services for authentication
            services.AddScoped<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
