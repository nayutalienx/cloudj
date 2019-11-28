using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CloudJ.API.Options;
using CloudJ.Infrastructure;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CloudJ.API
{
    public class Startup
    {
        private readonly JwtBearerOptions _jwtBearerOptions;
        private readonly UiOptions _uiOptions;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _jwtBearerOptions = configuration.GetSection("JwtBearer").Get<JwtBearerOptions>();
            _uiOptions = configuration.GetSection("UiOptions").Get<UiOptions>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = _jwtBearerOptions.Authority;
                    options.RequireHttpsMetadata = false;
                    options.Audience = _jwtBearerOptions.Audience;
                });

            


            //dependency injection
            services.Install();

            //mysql configuration
            services.AddDbContext<CloudjContext>(options => options.UseMySql(Configuration.GetConnectionString("ConnectionMySQL")));


            //swagger

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "CloudJ API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                    x.IncludeXmlComments(xmlPath);

                x.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                x.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } },
                    { "Basic", new string[]{ } }
                });

                //x.OperationFilter<SecurityRequirementsOperationFilter>(); to show access in swagger UI (may be the reason of not sending token)
                services.AddCors(options =>
                {
                    options.AddPolicy(_uiOptions.Name, policy =>
                    {
                        policy.WithOrigins(_uiOptions.Url)
                            .AllowAnyHeader()
                            .AllowAnyMethod();

                    });
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseAuthentication();
            app.UseCors(_uiOptions.Name);
            ConfigureSwagger(app);
            app.UseMiddleware<ExceptionHandler>();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
        private void ConfigureSwagger(IApplicationBuilder app)
        {
            Options.SwaggerOptions swaggerOptions = new Options.SwaggerOptions();

            Configuration.GetSection("SwaggerOptions").Bind(swaggerOptions);

            app.UseSwagger(option => option.RouteTemplate = swaggerOptions.JsonRoute);
            app.UseSwaggerUI(option => option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description));
        }
    }
}
