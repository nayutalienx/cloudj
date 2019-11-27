using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CloudJ.Client.Clients;
using CloudJ.Client.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudJ.Client
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        private readonly ApiClientOptions _apiClientOptions;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiClientOptions = _configuration.GetSection("ApiClient").Get<ApiClientOptions>();
        }

       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.Configure<ApiClientOptions>(_configuration.GetSection("ApiClient"));
            services.Configure<SolutionApiClientOptions>(_configuration.GetSection("SolutionApiClient"));
            services.Configure<BillingApiClientOptions>(_configuration.GetSection("BillingApiClient"));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = "cloudj-app";
            })
            .AddOpenIdConnect("oidc", options =>
            {
                //options.Authority = _openIdConnectOptions.Authority;
                options.RequireHttpsMetadata = false;

                //options.ClientId = _openIdConnectOptions.ClientId;
                //options.ClientSecret = _openIdConnectOptions.ClientSecret;
                //options.ResponseType = _openIdConnectOptions.ResponseType;

                options.SaveTokens = true;


                options.Scope.Add("cloudj-api");
                options.Scope.Add("role");
                options.Scope.Add("offline_access");
            });

            services.AddHttpClient<ISolutionApiClient, SolutionApiClient>(options =>
            {
                options.Timeout = TimeSpan.FromMinutes(1);
                options.BaseAddress = new Uri(_apiClientOptions.BaseUrl);
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddHttpClient<IBillingApiClient, BillingApiClient>(options =>
            {
                options.Timeout = TimeSpan.FromMinutes(1);
                options.BaseAddress = new Uri(_apiClientOptions.BaseUrl);
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
