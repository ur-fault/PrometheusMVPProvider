// See https://aka.ms/new-console-template for more information

using System.Diagnostics.Metrics;
using System.Reflection;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.Console;
using Prometheus;
using PrometheusMVPProvider;

var services = new ServiceCollection();
services.AddLogging(config => {
    config.AddConsole();
});

var builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options => options.ServiceName = "PrometheusMVPProvider")
    .ConfigureServices((_, services) => {
        LoggerProviderOptions.RegisterProviderOptions<ConsoleLoggerOptions, ConsoleLoggerProvider>(services);

        //services.AddSingleton<Metrics>();
        services.AddLocalMetrics();
        services.AddSingleton<TestService>();
        services.AddHostedService<WindowsBackgroundService>();
    });


var host = builder.Build();

var port = 5678;
using var metricServer = new KestrelMetricServer(port);

var logger = host.Services.GetRequiredService<ILogger<Program>>();

metricServer.Start();
logger.LogInformation("Metrics server started on port {Port}", port);
logger.LogInformation("Open metrics in browser: {Url}", $"http://localhost:{port}");

await host.RunAsync();