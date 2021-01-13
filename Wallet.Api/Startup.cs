using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Wallet.Api.Middlewares.ExceptionsMiddleware;
using Wallet.Data.Context.Contract;
using Wallet.Data.Context.Implementation;
using Wallet.Data.Contracts;
using Wallet.Data.Implementations;
using Wallet.Services.Contracts;
using Wallet.Services.Implementations;

namespace Wallet.Api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
              options.AddPolicy(MyAllowSpecificOrigins,
              builder =>
              {
                  builder
                      .AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
              })
              );

            services.AddControllers()
                .AddFluentValidation()
                .AddNewtonsoftJson();

            // Database dependencies
            services.AddScoped<IDbContext, ApplicationDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPurseRepository, PurseRepository>();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Service layer dependencies
            services.AddScoped<IAccessControlService, AccessControlService>();
            services.AddScoped<IPurseService, PurseService>();
            services.AddTransient<ICurrencyService, CurrencyService>();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Wallet.API",
                    Description = "This API is an example of programming applications using ASP.Net Core",
                });
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbContext dbContext)
        {
            dbContext.EnsureCreated();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wallet");
                c.RoutePrefix = string.Empty;
            });          

            app.UseHttpsRedirection();
            app.UseRouting();
            app.ConfigureCustomExceptionMiddleware();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
