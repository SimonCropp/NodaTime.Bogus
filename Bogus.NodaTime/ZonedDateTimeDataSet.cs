using System;
using NodaTime;

namespace Bogus.NodaTime
{
    /// <summary>
    /// Methods for generating <see cref="ZonedDateTime"/>s.
    /// </summary>
    public class ZonedDateTimeDataSet : DataSet
    {
        Func<DateTimeZone> defaultDateTimeZone;

        public ZonedDateTimeDataSet(Func<DateTimeZone> defaultDateTimeZone)
        {
            this.defaultDateTimeZone = defaultDateTimeZone;
        }

        /// <summary>
        /// Get a date in the past between <paramref name="reference"/> and years past that date.
        /// </summary>
        /// <param name="daysToGoBack">Days to go back from <paramref name="reference"/>. Default is 100.</param>
        /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
        public ZonedDateTime Past(int daysToGoBack = 100, ZonedDateTime? reference = null)
        {
            var max = ValueOrNow(reference);

            var totalTicks = TimeSpan.TicksPerDay * daysToGoBack;

            var partTicks = Random.Long(0, totalTicks);

            return max.PlusTicks(-partTicks);
        }

        ZonedDateTime ValueOrNow(ZonedDateTime? reference)
        {
            if (reference != null)
            {
                return reference.Value;
            }

            return Now();
        }

        ZonedDateTime Now()
        {
            var currentInstant = SystemClock.Instance.GetCurrentInstant();
            return new ZonedDateTime(currentInstant, defaultDateTimeZone());
        }

        /// <summary>
        /// Get a date and time that will happen soon.
        /// </summary>
        /// <param name="days">A date no more than N days ahead.</param>
        public ZonedDateTime Soon(int days = 1)
        {
            var currentInstant = Now();
            return Between(currentInstant, currentInstant.PlusTicks(days));
        }

        /// <summary>
        /// Get a date in the future between <paramref name="reference"/> and years forward of that date.
        /// </summary>
        /// <param name="daysToGoForward">Days to go forward from <paramref name="reference"/>. Default is 100.</param>
        /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
        public ZonedDateTime Future(int daysToGoForward = 100, ZonedDateTime? reference = null)
        {
            var min = ValueOrNow(reference);

            var totalTicks = TimeSpan.TicksPerDay * daysToGoForward;
            var partTicks = Random.Long(0, totalTicks);

            return min.PlusTicks(partTicks);
        }

        /// <summary>
        /// Get a random date between start and end.
        /// </summary>
        public ZonedDateTime Between(ZonedDateTime start, ZonedDateTime end)
        {
            //TODO: check for mis matched zones?
            var min = Instant.Min(start.ToInstant(), end.ToInstant());
            var max = Instant.Max(start.ToInstant(), end.ToInstant());

            var total = max - min;

            var partTicks = Random.Double() * total.TotalTicks;

            var part = Duration.FromTicks(partTicks);
            return new ZonedDateTime(min + part, start.Zone);
        }

        /// <summary>
        /// Get a random date/time within the last few days since now.
        /// </summary>
        /// <param name="days">Number of days to go back.</param>
        public ZonedDateTime Recent(int days = 1)
        {
            var currentInstant = SystemClock.Instance.GetCurrentInstant();

            var min = days == 0 ? currentInstant : currentInstant.Minus(Duration.FromDays(days));

            var totalTicks = (currentInstant - min).TotalTicks;

            //find % of the timespan
            var partTicks = Random.Double() * totalTicks;

            var part = Duration.FromTicks(partTicks);
            return new ZonedDateTime(currentInstant - part, defaultDateTimeZone());
        }
    }
}