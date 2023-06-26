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

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        var jwtSettings = Configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("Key"));


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
                ClockSkew = TimeSpan.Zero
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
            mongoIdentityOptions.UsersCollection = "Users";
            mongoIdentityOptions.RolesCollection = "Roles";
        });

        

        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddScoped<IMindbodyClient, MindbodyClient>();
        services.AddScoped<IMindbodyDataRepository, MindbodyDataRepository>();
        services.AddScoped<IMindbodyBatchRepository, MindbodyBatchRepository>();

        services.AddScoped<IMindbodyBatchReportService, MindbodyBatchReportService>();
        services.AddScoped<IMindbodyDataService, MindbodyDataService>();
        services.AddScoped<IMindbodyClientService, MindbodyClientService>();
        services.AddScoped<IMindbodySettingsService, MindbodySettingsService>();
        services.AddScoped<IUserService, UserService>();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        
        

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();
        app.UseAuthentication();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}