using System;
using NodaTime;

namespace Bogus.NodaTime
{
    /// <summary>
    /// Methods for generating <see cref="LocalTime"/>s.
    /// </summary>
    public class LocalTimeDataSet : DataSet
    {
        Func<DateTimeZone> dateTimeZoneBuilder;

        public LocalTimeDataSet(Func<DateTimeZone> dateTimeZoneBuilder)
        {
            this.dateTimeZoneBuilder = dateTimeZoneBuilder;
        }

        /// <summary>
        /// Get a date in the past between <paramref name="reference"/> and years past that date.
        /// </summary>
        /// <param name="hoursToGoBack">Hours to go back from <paramref name="reference"/>. Default is 10.</param>
        /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
        public LocalTime Past(int hoursToGoBack = 10, LocalTime? reference = null)
        {
            var min = ValueOrNull(reference);

            var totalTicks = TimeSpan.TicksPerHour* hoursToGoBack;

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
        /// <param name="hours">Maximum hours to go ahead.</param>
        public LocalTime Soon(int hours = 1)
        {
            var now = Now();
            return Between(now, now.PlusHours(hours));
        }

        /// <summary>
        /// Get a date in the future between <paramref name="reference"/> and years forward of that date.
        /// </summary>
        /// <param name="hoursToGoForward">Hours to go forward from <paramref name="reference"/>. Default is 10.</param>
        /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
        public LocalTime Future(int hoursToGoForward = 10, LocalTime? reference = null)
        {
            var min = ValueOrNull(reference);

            var totalTicks = TimeSpan.TicksPerHour * hoursToGoForward;

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

            var total = max - min;

            var partTicks = Random.Double() * total.ToDuration().TotalTicks;

            return min.PlusTicks(Convert.ToInt64(partTicks));
        }

        /// <summary>
        /// Get a random date/time within the last few days since now.
        /// </summary>
        /// <param name="hours">Number of hours to go back.</param>
        public LocalTime Recent(int hours = 1)
        {
            var now = Now();

            var totalTicks = TimeSpan.TicksPerHour * hours;

            var partTicks = Random.Long(0, totalTicks);

            return now.PlusTicks(-partTicks);
        }
    }
}