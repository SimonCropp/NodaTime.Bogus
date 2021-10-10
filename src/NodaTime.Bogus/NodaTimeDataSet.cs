using NodaTime;

namespace Bogus.NodaTime;

public class NodaTimeDataSet :
    DataSet
{
    List<string> calendarIds = global::NodaTime.CalendarSystem.Ids.ToList();
    public InstantDataSet Instant { get; }
    public LocalDateDataSet LocalDate { get; }
    public LocalDateTimeDataSet LocalDateTime { get; }
    public LocalTimeDataSet LocalTime { get; }
    public ZonedDateTimeDataSet ZonedDateTime { get; }

    public static Func<NodaTimeDataSet, DateTimeZone> DateTimeZoneBuilder { get; set; } = set => set.DateTimeZone();

    public static void AlwaysUseDtcDateTimeZone()
    {
        DateTimeZoneBuilder = _ => global::NodaTime.DateTimeZone.Utc;
    }

    public static void AlwaysUseSystemDefaultDateTimeZone()
    {
        DateTimeZoneBuilder = _ => DateTimeZoneProviders.Tzdb.GetSystemDefault();
    }

    public NodaTimeDataSet() : this(null)
    {
    }

    public NodaTimeDataSet(Func<DateTimeZone>? dateTimeZoneBuilder)
    {
        dateTimeZoneBuilder ??= () => NodaTimeDataSet.DateTimeZoneBuilder(this);

        Instant = new()
        {
            Random = Random
        };
        LocalDate = new(dateTimeZoneBuilder)
        {
            Random = Random
        };
        LocalDateTime = new(dateTimeZoneBuilder)
        {
            Random = Random
        };
        LocalTime = new(dateTimeZoneBuilder)
        {
            Random = Random
        };
        ZonedDateTime = new(dateTimeZoneBuilder)
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
    public Period Period(Period? maximum = null, LocalDateTime? anchor = null, PeriodUnits units = global::NodaTime.PeriodUnits.AllUnits)
    {
        var anchorTime = anchor ?? LocalDateTime.Now();
        var span = maximum ?? global::NodaTime.Period.FromDays(7);

        var periodTicks = global::NodaTime.Period.Between(anchorTime, anchorTime + span, global::NodaTime.PeriodUnits.Ticks);
        var randomTicks = global::NodaTime.Period.FromTicks(Random.Long(0, periodTicks.Ticks));

        return global::NodaTime.Period.Between(anchorTime, anchorTime + randomTicks, units);
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