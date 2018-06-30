using System;
using NodaTime;

namespace Bogus.NodaTime
{
    /// <summary>
    /// Methods for generating <see cref="LocalDate"/>s.
    /// </summary>
    public class LocalDateDataSet : DataSet
    {
        Func<DateTimeZone> defaultDateTimeZone;

        public LocalDateDataSet(Func<DateTimeZone> defaultDateTimeZone)
        {
            this.defaultDateTimeZone = defaultDateTimeZone;
        }

        /// <summary>
        /// Get a date in the past between <paramref name="reference"/> and years past that date.
        /// </summary>
        /// <param name="daysToGoBack">Days to go back from <paramref name="reference"/>. Default is 100.</param>
        /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
        public LocalDate Past(int daysToGoBack = 100, LocalDate? reference = null)
        {
            var maxDate = ValueOrNull(reference);

            var minDate = maxDate.Minus(Period.FromDays(daysToGoBack));

            var totalTicks = (maxDate - minDate).Ticks;

            //find % of the timespan
            var partTicks = Random.Double() * totalTicks;

            var partTimeSpan = Period.FromTicks(Convert.ToInt64(partTicks));

            return maxDate - partTimeSpan;
        }

        LocalDate ValueOrNull(LocalDate? reference)
        {
            if (reference == null)
            {
                return Now();
            }

            return reference.Value;
        }

        LocalDate Now()
        {
            var currentInstant = SystemClock.Instance.GetCurrentInstant();
            return currentInstant.InZone(defaultDateTimeZone()).Date;
        }

        /// <summary>
        /// Get a date and time that will happen soon.
        /// </summary>
        /// <param name="days">A date no more than N days ahead.</param>
        public LocalDate Soon(int days = 1)
        {
            var now = Now();
            return Between(now, now.Plus(Period.FromDays(days)));
        }

        /// <summary>
        /// Get a date in the future between <paramref name="reference"/> and years forward of that date.
        /// </summary>
        /// <param name="daysToGoForward">Days to go forward from <paramref name="reference"/>. Default is 100.</param>
        /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
        public LocalDate Future(int daysToGoForward = 100, LocalDate? reference = null)
        {
            var minDate = ValueOrNull(reference);

            var maxDate = minDate.Plus(Period.FromDays(daysToGoForward));

            var totalTicks = (maxDate - minDate).Ticks;

            //find % of the timespan
            var partTicks = Random.Double() * totalTicks;

            var partTimeSpan = Period.FromTicks(Convert.ToInt64(partTicks));

            return minDate + partTimeSpan;
        }

        /// <summary>
        /// Get a random date between start and end.
        /// </summary>
        public LocalDate Between(LocalDate start, LocalDate end)
        {
            var min = LocalDate.Min(start, end);
            var max = LocalDate.Max(start, end);

            var total = max - min;

            var randomDays = Random.Int(0, (int)total.ToDuration().TotalDays);

            return min.PlusDays(randomDays);
        }

        /// <summary>
        /// Get a random date/time within the last few days since now.
        /// </summary>
        /// <param name="days">Number of days to go back.</param>
        public LocalDate Recent(int days = 10)
        {
            var now = Now();

            var minDate = days == 0 ? now : now.Minus(Period.FromDays(days));

            var randomDays = Random.Int(0, (int)(now - minDate).ToDuration().TotalDays);

            return now.PlusDays(-randomDays);
        }
    }
}