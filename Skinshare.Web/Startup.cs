using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.AspNetCore.DataProtection.SSM;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Skinshare.Data;
using Skinshare.Data.Interfaces;
using Skinshare.Data.Services;

namespace Skinshare.Web
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
            services.AddRazorPages();
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            services.AddControllers(config => { config.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
                .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerDocument();
            services.AddHealthChecks();
            services.AddDbContextPool<RoutineContext>(options => { options.UseNpgsql(Configuration.GetConnectionString("Skinshare")); });
            services.AddScoped(typeof(IAsyncRepository<>), typeof(SqlRepository<>));
            services.AddScoped<IRoutineService, RoutineService>();
            services.AddScoped<IStepService, StepService>();
            services.AddDataProtection()
                .PersistKeysToAWSSystemsManager("/Skinshare/DataProtection", options =>
                {
                    options.TierStorageMode = TierStorageMode.StandardOnly;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "ClientApp/dist")),
                    RequestPath = "/ClientApp/dist"
                }); 
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();
            
            app.Use(CsrfMiddleware);

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health");
            });

            if (env.IsDevelopment())
            {
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = null;
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                });
            }
            
            async Task CsrfMiddleware(HttpContext context, Func<Task> next)
            {
                var path = context.Request.Path.Value;
                if (string.Equals(path, "/Routines/Create", StringComparison.OrdinalIgnoreCase))
                {
                    // The request token can be sent as a JavaScript-readable cookie, 
                    // and Angular uses it by default.
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, 
                        new CookieOptions() { HttpOnly = false });
                }

                await next.Invoke();
            }
        }
        
    }
}