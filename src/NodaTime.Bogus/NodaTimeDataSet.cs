using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;

namespace Bogus.NodaTime
{
    public class NodaTimeDataSet : DataSet
    {
        List<string> calendarIds = global::NodaTime.CalendarSystem.Ids.ToList();
        public InstantDataSet Instant { get; }
        public LocalDateDataSet LocalDate { get; }
        public LocalDateTimeDataSet LocalDateTime { get; }
        public LocalTimeDataSet LocalTime { get; }
        public ZonedDateTimeDataSet ZonedDateTime { get; }

        public static Func<NodaTimeDataSet, DateTimeZone> DateTimeZoneBuilder
        {
            get => dateTimeZoneBuilder;
            set
            {
                Guard.AgainstNull(value, nameof(value));
                dateTimeZoneBuilder = value;
            }
        }

        static Func<NodaTimeDataSet, DateTimeZone> dateTimeZoneBuilder = set => set.DateTimeZone();

        public static void AlwaysUseDtcDateTimeZone()
        {
            dateTimeZoneBuilder = x => global::NodaTime.DateTimeZone.Utc;
        }

        public static void AlwaysUseSystemDefaultDateTimeZone()
        {
            dateTimeZoneBuilder = x => DateTimeZoneProviders.Tzdb.GetSystemDefault();
        }

        public NodaTimeDataSet() : this(null)
        {
        }

        public NodaTimeDataSet(Func<DateTimeZone> dateTimeZoneBuilder)
        {
            if (dateTimeZoneBuilder == null)
            {
                dateTimeZoneBuilder = () => NodaTimeDataSet.dateTimeZoneBuilder(this);
            }

            Instant = new InstantDataSet
            {
                Random = Random
            };
            LocalDate = new LocalDateDataSet(dateTimeZoneBuilder)
            {
                Random = Random
            };
            LocalDateTime = new LocalDateTimeDataSet(dateTimeZoneBuilder)
            {
                Random = Random
            };
            LocalTime = new LocalTimeDataSet(dateTimeZoneBuilder)
            {
                Random = Random
            };
            ZonedDateTime = new ZonedDateTimeDataSet(dateTimeZoneBuilder)
            {
                Random = Random
            };
        }

        /// <summary>
        /// Get a random <see cref="Offset"/>.
        /// </summary>
        public Offset Offset()
        {
            var offset = Random.Long(global::NodaTime.Offset.MinValue.Ticks, global::NodaTime.Offset.MaxValue.Ticks);
            return global::NodaTime.Offset.FromTicks(offset);
        }

        /// <summary>
        /// Get a random <see cref="DateTimeZone"/>.
        /// </summary>
        public DateTimeZone DateTimeZone()
        {
            return global::NodaTime.DateTimeZone.ForOffset(Offset());
        }

        /// <summary>
        /// Get a random <see cref="CalendarSystem"/>.
        /// </summary>
        public CalendarSystem CalendarSystem()
        {
            var id = Random.ListItem(calendarIds);
            return global::NodaTime.CalendarSystem.ForId(id);
        }

        /// <summary>
        /// Get a random <see cref="PeriodUnits"/>.
        /// </summary>
        public PeriodUnits PeriodUnits()
        {
            return Random.Enum<PeriodUnits>();
        }

        /// <summary>
        /// Get a random <see cref="IsoDayOfWeek"/>.
        /// </summary>
        public IsoDayOfWeek IsoDayOfWeek()
        {
            return Random.Enum<IsoDayOfWeek>();
        }

        /// <summary>
        /// Get a random <see cref="Period"/>. Default 1 week/7 days.
        /// </summary>
        public Period Period(Period maximum = null)
        {
            Period span;
            if (maximum == null)
            {
                span = global::NodaTime.Period.FromDays(7);
            }
            else
            {
                span = maximum;
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
            if (maximum == null)
            {
                span = global::NodaTime.Duration.FromDays(7);
            }
            else
            {
                span = maximum.Value;
            }

            var partTimeSpanTicks = Random.Double() * span.TotalTicks;

            return global::NodaTime.Duration.FromTicks(partTimeSpanTicks);
        }
    }
}