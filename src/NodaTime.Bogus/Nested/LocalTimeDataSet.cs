using NodaTime;

namespace Bogus.NodaTime;

/// <summary>
/// Methods for generating <see cref="LocalTime"/>s.
/// </summary>
public class LocalTimeDataSet(Func<DateTimeZone> dateTimeZoneBuilder) :
    DataSet
{
    /// <summary>
    /// Get a date in the past between <paramref name="reference"/> and years past that date.
    /// </summary>
    /// <param name="minutesToGoBack">Minutes to go back from <paramref name="reference"/>. Default is 10.</param>
    /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
    public LocalTime Past(int minutesToGoBack = 10, LocalTime? reference = null)
    {
        var min = ValueOrNull(reference);

        var totalTicks = TimeSpan.TicksPerMinute * minutesToGoBack;

        var partTicks = Random.Long(0, totalTicks);

        return min.PlusTicks(-partTicks);
    }

    LocalTime ValueOrNull(LocalTime? reference)
    {
        if (reference == null)
        {
            return Now();
        }

        return reference.Value;
    }

    /// <summary>
    /// Get the current <see cref="LocalTime"/> that respects <code>Func&lt;DateTimeZone&gt; dateTimeZoneBuilder</code>
    /// </summary>
    public LocalTime Now()
    {
        var currentInstant = SystemClock.Instance.GetCurrentInstant();
        return currentInstant.InZone(dateTimeZoneBuilder()).TimeOfDay;
    }

    /// <summary>
    /// Get a date and time that will happen soon.
    /// </summary>
    /// <param name="minutes">Maximum minutes to go ahead.</param>
    public LocalTime Soon(int minutes = 1)
    {
        var now = Now();
        return Between(now, now.PlusMinutes(minutes));
    }

    /// <summary>
    /// Get a date in the future between <paramref name="reference"/> and years forward of that date.
    /// </summary>
    /// <param name="minutesToGoForward">Minutes to go forward from <paramref name="reference"/>. Default is 10.</param>
    /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
    public LocalTime Future(int minutesToGoForward = 10, LocalTime? reference = null)
    {
        var min = ValueOrNull(reference);

        var totalTicks = TimeSpan.TicksPerMinute * minutesToGoForward;

        var partTicks = Random.Long(0, totalTicks);

        return min.PlusTicks(partTicks);
    }

    /// <summary>
    /// Get a random date between start and end.
    /// </summary>
    public LocalTime Between(LocalTime start, LocalTime end)
    {
        var min = LocalTime.Min(start, end);
        var max = LocalTime.Max(start, end);

        var periodBetween = Period.Between(min, max, PeriodUnits.Ticks);

        return min.PlusTicks(Random.Long(0, periodBetween.Ticks));
    }

    /// <summary>
    /// Get a random time within the last few minutes since now.
    /// </summary>
    /// <param name="minutes">Number of minutes to go back.</param>
    public LocalTime Recent(int minutes = 1)
    {
        var now = Now();

        var totalTicks = TimeSpan.TicksPerMinute * minutes;

        var partTicks = Random.Long(0, totalTicks);

        return now.PlusTicks(-partTicks);
    }
}