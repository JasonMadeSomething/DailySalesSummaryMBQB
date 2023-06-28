using DailySalesSummary.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using DailySalesSummary.Controllers;
using DailySalesSummary.Services;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AspNetCore.Identity.Mongo;
using DailySalesSummary.Models;
using AspNetCore.Identity.Mongo.Model;
using DailySalesSummary.Helpers;
using Mindbody.Helpers;
using System.Security.Claims;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        IConfiguration jwtSettings = Configuration.GetSection("JwtSettings");
        byte[] key = Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("Key"));

        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                ValidateAudience = true,
                ValidAudience = jwtSettings.GetValue<string>("Audience"),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RoleClaimType = ClaimTypes.Role
            };
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    System.Diagnostics.Debug.WriteLine("Authentication failed:" + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    System.Diagnostics.Debug.WriteLine("Auth Worked" + context.SecurityToken);
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    System.Diagnostics.Debug.WriteLine("OnChallenge: " + context.Error);
                    return Task.CompletedTask;
                }
            };
        });
        services.AddSingleton<IMongoClient, MongoClient>(s =>
        {
            var connectionString = Configuration.GetConnectionString("MongoDb");
            return new MongoClient(connectionString);
        });
        services.AddHttpClient("Mindbody", client =>
        {
            
            client.BaseAddress = new Uri(Configuration.GetSection("MindbodyAPI:BaseUrl").Value);
            
        });
        
        services.AddIdentityMongoDbProvider<User, MongoRole>(identityOptions =>
        {
            identityOptions.Password.RequiredLength = 8;
            identityOptions.Password.RequireDigit = true;
            identityOptions.Password.RequireLowercase = true;
            identityOptions.Password.RequireUppercase = true;
            identityOptions.Password.RequireNonAlphanumeric = true;
        }, mongoIdentityOptions =>
        {
            mongoIdentityOptions.ConnectionString = Configuration.GetConnectionString("MongoDb");
            Configuration.GetConnectionString("DatabaseName");
            mongoIdentityOptions.UsersCollection = "users";
            mongoIdentityOptions.RolesCollection = "roles";
        });

        services.AddScoped<IJWTGenerator, JWTGenerator>();
        services.AddScoped<AdminUserEnsurer>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMindbodyClient, MindbodyClient>();
        services.AddScoped<IMindbodyDataRepository, MindbodyDataRepository>();
        services.AddScoped<IMindbodyBatchRepository, MindbodyBatchRepository>();
        services.AddScoped<IMindbodyBatchService, MindbodyBatchService>();

        services.AddScoped<IMindbodyBatchReportService, MindbodyBatchReportService>();
        services.AddScoped<IMindbodyDataService, MindbodyDataService>();
        services.AddScoped<IMindbodyClientService, MindbodyClientService>();
        services.AddScoped<IMindbodySettingsService, MindbodySettingsService>();
        services.AddScoped<IUserService, UserService>();
        services.AddControllers();
    }

    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, AdminUserEnsurer adminUserEnsurer)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        adminUserEnsurer.EnsureCreatedAsync().Wait();


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