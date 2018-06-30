using NodaTime;

namespace Bogus.NodaTime
{
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
            var maxDate = reference ?? SystemClock.Instance.GetCurrentInstant();

            var minDate = maxDate.Minus(Duration.FromDays(daysToGoBack));

            var totalTicks = (maxDate - minDate).TotalTicks;

            //find % of the timespan
            var partTicks = Random.Double() * totalTicks;

            var partTimeSpan = Duration.FromTicks(partTicks);

            return maxDate - partTimeSpan;
        }

        /// <summary>
        /// Get a date and time that will happen soon.
        /// </summary>
        /// <param name="days">A date no more than N days ahead.</param>
        public Instant Soon(int days = 1)
        {
            var currentInstant = SystemClock.Instance.GetCurrentInstant();
            return Between(currentInstant, currentInstant.Plus(Duration.FromDays(days)));
        }

        /// <summary>
        /// Get a date in the future between <paramref name="reference"/> and years forward of that date.
        /// </summary>
        /// <param name="daysToGoForward">Days to go forward from <paramref name="reference"/>. Default is 100.</param>
        /// <param name="reference">The date to start calculations. Default is SystemClock.Instance.GetCurrentInstant().</param>
        public Instant Future(int daysToGoForward = 100, Instant? reference = null)
        {
            var minDate = reference ?? SystemClock.Instance.GetCurrentInstant();

            var maxDate = minDate.Plus(Duration.FromDays(daysToGoForward));

            var totalTicks = (maxDate - minDate).TotalTicks;

            //find % of the timespan
            var partTicks = Random.Double() * totalTicks;

            var partTimeSpan = Duration.FromTicks(partTicks);

            return minDate + partTimeSpan;
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

            var minDate = days == 0 ? currentInstant : currentInstant.Minus(Duration.FromDays(days));

            var totalTicks = (currentInstant - minDate).TotalTicks;

            //find % of the timespan
            var partTicks = Random.Double() * totalTicks;

            var part = Duration.FromTicks(partTicks);

            return currentInstant - part;
        }
    }
}