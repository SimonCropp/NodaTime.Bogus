using System;
using NodaTime;

namespace Bogus.NodaTime
{
    /// <summary>
    /// Methods for generating <see cref="LocalDate"/>s.
    /// </summary>
    public class LocalDateDataSet : DataSet
    {
        Func<DateTimeZone> dateTimeZoneBuilder;

        public LocalDateDataSet(Func<DateTimeZone> dateTimeZoneBuilder)
        {
            this.dateTimeZoneBuilder = dateTimeZoneBuilder;
        }

        /// <summary>
        /// Get a date in the past between <paramref name="reference"/> and years past that date.
        /// </summary>
        /// <param name="daysToGoBack">Days to go back from <paramref name="reference"/>. Default is 100.</param>
        /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
        public LocalDate Past(int daysToGoBack = 100, LocalDate? reference = null)
        {
            var max = ValueOrNull(reference);
            var random = Random.Int(0, daysToGoBack);
            return max.PlusDays(-random);
        }

        LocalDate ValueOrNull(LocalDate? reference)
        {
            if (reference == null)
            {
                return Now();
            }

            return reference.Value;
        }

        /// <summary>
        /// Get the current <see cref="LocalDate"/> that respects <code>Func&lt;DateTimeZone&gt; dateTimeZoneBuilder</code>
        /// </summary>
        public LocalDate Now()
        {
            var currentInstant = SystemClock.Instance.GetCurrentInstant();
            return currentInstant.InZone(dateTimeZoneBuilder()).Date;
        }

        /// <summary>
        /// Get a date and time that will happen soon.
        /// </summary>
        /// <param name="days">A date no more than N days ahead.</param>
        public LocalDate Soon(int days = 10)
        {
            var now = Now();
            return Between(now, now.PlusDays(days));
        }

        /// <summary>
        /// Get a date in the future between <paramref name="reference"/> and years forward of that date.
        /// </summary>
        /// <param name="daysToGoForward">Days to go forward from <paramref name="reference"/>. Default is 100.</param>
        /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
        public LocalDate Future(int daysToGoForward = 100, LocalDate? reference = null)
        {
            var min = ValueOrNull(reference);

            var random = Random.Int(0, daysToGoForward);

            return min.PlusDays(random);
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

            var randomDays = Random.Int(0, days);

            return now.PlusDays(-randomDays);
        }
    }
}