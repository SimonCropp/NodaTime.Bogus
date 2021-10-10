using NodaTime;

namespace Bogus.NodaTime;

/// <summary>
/// Methods for generating <see cref="Instant"/>s.
/// </summary>
public class InstantDataSet : DataSet
{
    /// <summary>
    /// Get a date in the past between <paramref name="reference"/> and years past that date.
    /// </summary>
    /// <param name="daysToGoBack">Days to go back from <paramref name="reference"/>. Default is 100.</param>
    /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
    public Instant Past(int daysToGoBack = 100, Instant? reference = null)
    {
        var max = reference ?? SystemClock.Instance.GetCurrentInstant();

        var totalTicks = TimeSpan.TicksPerDay * daysToGoBack;

        var partTicks = Random.Long(0, totalTicks);

        return max.PlusTicks(-partTicks);
    }

    /// <summary>
    /// Get a date and time that will happen soon.
    /// </summary>
    /// <param name="days">A date no more than N days ahead.</param>
    public Instant Soon(int days = 1)
    {
        var currentInstant = SystemClock.Instance.GetCurrentInstant();
        return Between(currentInstant, currentInstant.PlusTicks(days));
    }

    /// <summary>
    /// Get a date in the future between <paramref name="reference"/> and years forward of that date.
    /// </summary>
    /// <param name="daysToGoForward">Days to go forward from <paramref name="reference"/>. Default is 100.</param>
    /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
    public Instant Future(int daysToGoForward = 100, Instant? reference = null)
    {
        var min = reference ?? SystemClock.Instance.GetCurrentInstant();

        var totalTicks = TimeSpan.TicksPerDay * daysToGoForward;
        var partTicks = Random.Long(0, totalTicks);

        return min.PlusTicks(partTicks);
    }

    /// <summary>
    /// Get a random date between start and end.
    /// </summary>
    public Instant Between(Instant start, Instant end)
    {
        var min = Instant.Min(start, end);
        var max = Instant.Max(start, end);

        var total = max - min;

        var partTicks = Random.Double() * total.TotalTicks;

        var part = Duration.FromTicks(partTicks);

        return min + part;
    }

    /// <summary>
    /// Get a random date/time within the last few days since now.
    /// </summary>
    /// <param name="days">Number of days to go back.</param>
    public Instant Recent(int days = 1)
    {
        var currentInstant = SystemClock.Instance.GetCurrentInstant();

        var min = days == 0 ? currentInstant : currentInstant.Minus(Duration.FromDays(days));

        var totalTicks = (currentInstant - min).TotalTicks;

        //find % of the timespan
        var partTicks = Random.Double() * totalTicks;

        var part = Duration.FromTicks(partTicks);

        return currentInstant - part;
    }
}