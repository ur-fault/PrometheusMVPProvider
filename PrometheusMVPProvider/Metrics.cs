using System.Diagnostics.Metrics;

namespace PrometheusMVPProvider;
internal class Metrics
{
    private readonly Counter<int> _numbersChecked;
    private readonly Counter<int> _primesFound;

    public Metrics() {
        var meter = new Meter("Prometheus");
        _numbersChecked = meter.CreateCounter<int>("numbers-checked");
        _primesFound = meter.CreateCounter<int>("primes-found");
    }

    public void AddNumberChecked() => _numbersChecked.Add(1);

    public void AddPrimesFound(int count) => _primesFound.Add(count);
}
