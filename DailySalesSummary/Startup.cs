using DailySalesSummary.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using DailySalesSummary.Controllers;
using DailySalesSummary.Services;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IMongoClient, MongoClient>(s =>
        {
            var connectionString = Configuration.GetConnectionString("MongoDb");
            return new MongoClient(connectionString);
        });
        services.AddHttpClient("Mindbody", client =>
        {
            
            client.BaseAddress = new Uri(Configuration.GetSection("MindbodyAPI:BaseUrl").Value);
            
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

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}