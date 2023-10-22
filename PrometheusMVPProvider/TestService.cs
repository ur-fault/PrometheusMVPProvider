using System.Diagnostics.Metrics;

namespace PrometheusMVPProvider;
internal class TestService
{
    private Counter<int> _numbersCheckedCounter;

    public TestService(Meter meter) {
        _numbersCheckedCounter = meter.CreateCounter<int>("numbers-checked");
    }

    public int[] FindPrimes(int from, int to, CancellationToken? ct = null) {
        var primes = new List<int>();
        for (var i = from; i <= to; i++) {
            if (ct?.IsCancellationRequested ?? false)
                throw new OperationCanceledException();

            if (IsPrime(i))
                primes.Add(i);
        }
        return primes.ToArray();
    }

    public bool IsPrime(int number) {
        _numbersCheckedCounter.Add(1);

        if (number < 2)
            return false;

        if (number == 2)
            return true;

        if (number % 2 == 0)
            return false;

        for (var i = 3; i * i <= number; i += 2) {
            if (number % i == 0)
                return false;
        }
        return true;
    }
}
