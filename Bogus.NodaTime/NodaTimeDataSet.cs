using System;
using NodaTime;

namespace Bogus.NodaTime
{
    public class NodaTimeDataSet : DataSet
    {
        public InstantDataSet Instant { get; }

        public NodaTimeDataSet()
        {
            Instant = new InstantDataSet
            {
                Random = Random
            };
        }

        /// <summary>
        /// Get a random <see cref="Period"/>. Default 1 week/7 days.
        /// </summary>
        public Period Period(Period maximum = null)
        {
            Period span;
            if (maximum != null)
            {
                span = maximum;
            }
            else
            {
                span = global::NodaTime.Period.FromDays(7);
            }

            var partTimeSpanTicks = Random.Double() * span.ToDuration().TotalTicks;

            return global::NodaTime.Period.FromTicks(Convert.ToInt64(partTimeSpanTicks));
        }

        /// <summary>
        /// Get a random <see cref="Duration"/>. Default 1 week/7 days.
        /// </summary>
        public Duration Duration(Duration? maximum = null)
        {
            Duration span;
            if (maximum != null)
            {
                span = maximum.Value;
            }
            else
            {
                span = global::NodaTime.Duration.FromDays(7);
            }

            var partTimeSpanTicks = Random.Double() * span.TotalTicks;

            return global::NodaTime.Duration.FromTicks(partTimeSpanTicks);
        }
    }
}