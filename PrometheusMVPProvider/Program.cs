// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.Console;
using PrometheusMVPProvider;

var services = new ServiceCollection();
services.AddLogging(config => {
    config.AddConsole();
});

var builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options => options.ServiceName = "PrometheusMVPProvider")
    .ConfigureServices((context, services) => {
        LoggerProviderOptions.RegisterProviderOptions<ConsoleLoggerOptions, ConsoleLoggerProvider>(services);

        services.AddSingleton<TestService>();
        services.AddHostedService<WindowsBackgroundService>();
    });

var host = builder.Build();
await host.RunAsync();