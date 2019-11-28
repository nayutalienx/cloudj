using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer.DataAccessLayer;
using IdentityServer.Objects.AutoMapperProfiles;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        private readonly string _connectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config => {
                config.AddProfile(new UserProfile());
                config.AddProfile(new RoleProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
            services.AddTransient<IRepository<PhotoCaptcha>, Repository<PhotoCaptcha>>();
            services.AddControllersWithViews();
            services.AddIdentity(_connectionString);
            services.AddIdentityServer(_connectionString);
            services.AddAuthentication()
            //    .AddGoogle(options =>
            //{
            //    options.ClientId = "373625078064-qi3ah5b94smnpu7fp719ep1apsc7lqqk.apps.googleusercontent.com";
            //    options.ClientSecret = "E_-jKDm5MPQqlIS9eZBMGNmB";
            //})
            //    .AddFacebook(options =>
            //    {
            //        options.AppId = "1698412483628061";
            //        options.AppSecret = "2e2d873fef6f70e0c580fa245b1bf06e";
            //    })
                .AddVkontakte(options =>
                {
                    options.ClientId = "7211183";
                    options.ClientSecret = "1BeWX0qbwH3QsQ8oMOLa";
                });
           
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseMiddleware<ExceptionHandler>();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }

    static class ServiceCollectionExtensions {
        public static IServiceCollection AddIdentity(this IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<IdentityContext>(options => options.UseSqlite(connectionString));
            services.AddDbContext<IdentityContext>(options => options.UseMySql(connectionString));
            //services.AddDbContext<ConfigurationDbContext>(options => options.UseMySql(connectionString));
            //services.AddDbContext<PersistedGrantDbContext>(options => options.UseMySql(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            return services;
        }

        public static IServiceCollection AddIdentityServer(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = Assembly.GetAssembly(typeof(Startup)).GetName().Name;
            var builder = services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                    options.EnableTokenCleanup = true;
                });
            builder.AddDeveloperSigningCredential();
            return services;

        }
    }
}
