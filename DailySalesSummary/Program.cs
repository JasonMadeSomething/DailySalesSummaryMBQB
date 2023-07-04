using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureServices((IServiceCollection services) =>
            {
                services.AddOpenTelemetry()
                    .WithTracing(builder => builder
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddJaegerExporter(options =>
                        {
                            options.AgentHost = "localhost";
                            options.AgentPort = 6831;
                        })
                        .AddConsoleExporter()
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("DailySalesSummary"))
                    )
                    .WithMetrics(builder => builder
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddConsoleExporter()
                    );
            });
}