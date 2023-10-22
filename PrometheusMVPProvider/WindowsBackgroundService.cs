using System.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PrometheusMVPProvider;
partial class WindowsBackgroundService : BackgroundService
{
    private readonly ILogger<WindowsBackgroundService> _logger;
    private readonly TestService _testService;
    private readonly Metrics _metrics;

    public WindowsBackgroundService(ILogger<WindowsBackgroundService> logger, TestService testService, Metrics metrics) {
        _logger = logger;
        _testService = testService;
        _metrics = metrics;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        try {
            while (!stoppingToken.IsCancellationRequested) {
                var from = Random.Shared.Next(1, 1024 * 1024 * 1024);
                var to = Random.Shared.Next(from + 1, from + 1024 * 1024);

                _logger.LogInformation("Finding primes from {From} to {To}", from, to);
                var primes = await Task.Run(() => _testService.FindPrimes(from, to, ct: stoppingToken), stoppingToken);
                _logger.LogInformation("Found {Count} from {From} to {To}", primes.Length, from, to);

                _metrics.AddPrimesFound(primes.Length);

                await Task.Delay(1000, stoppingToken);
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) {
            _logger.LogError(ex, "Error occurred: {Message}", ex.Message);

            Environment.Exit(1);
        }
    }
}
