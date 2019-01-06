using System;
using Bogus.NodaTime;
using FluentAssertions;
using NodaTime;
using Xunit;

public class ZonedDateTimeDataSetTest : SeededTest
{
    public ZonedDateTimeDataSetTest()
    {
        dataSet = new ZonedDateTimeDataSet(() => DateTimeZone.Utc);
    }

    ZonedDateTimeDataSet dataSet;

    [Fact]
    public void Future()
    {
        var starting = new ZonedDateTime(Instant.FromUtc(2015,6,6,4,17,41), DateTimeZone.Utc);

        var zonedDateTime = dataSet.Future(reference: starting).ToDateTimeUtc();
        zonedDateTime.Should()
            .BeOnOrBefore(starting.Plus(Duration.FromDays(100)).ToDateTimeUtc())
            .And
            .BeOnOrAfter(starting.ToDateTimeUtc());
    }

    [Fact]
    public void Future_with_options()
    {
        var starting = new ZonedDateTime(Instant.FromUtc(2015, 6, 6, 4, 17, 41), DateTimeZone.Utc);
        dataSet.Future(reference: starting, daysToGoForward: 500).ToDateTimeUtc().Should()
            .BeOnOrBefore(starting.Plus(Duration.FromDays(500)).ToDateTimeUtc())
            .And
            .BeOnOrAfter(starting.ToDateTimeUtc());
    }

    [Fact]
    public void Past()
    {
        var starting = new ZonedDateTime(Instant.FromUtc(2015, 6, 6, 4, 17, 41), DateTimeZone.Utc);
        dataSet.Past(reference: starting).ToDateTimeUtc().Should()
            .BeOnOrBefore(starting.ToDateTimeUtc())
            .And
            .BeOnOrAfter(starting.Minus(Duration.FromDays(100)).ToDateTimeUtc());
    }

    [Fact]
    public void Past_0_days_results_in_Random_time()
    {
        var now = SystemClock.Instance.GetCurrentInstant();
        dataSet.Recent(0).ToDateTimeUtc().Should()
            .BeOnOrBefore(DateTime.UtcNow)
            .And
            .BeOnOrAfter(now.Minus(Duration.FromDays(1)).ToDateTimeUtc());
    }

    [Fact]
    public void Past_with_custom_options()
    {
        var starting = new ZonedDateTime(Instant.FromUtc(2015, 6, 6, 4, 17, 41), DateTimeZone.Utc);
        dataSet.Past(reference: starting, daysToGoBack: 500).ToDateTimeUtc().Should()
            .BeOnOrBefore(starting.ToDateTimeUtc())
            .And
            .BeOnOrAfter(starting.Minus(Duration.FromDays(500)).ToDateTimeUtc());
    }

    [Fact]
    public void Recently()
    {
        var start = SystemClock.Instance.GetCurrentInstant();
        dataSet.Recent().ToDateTimeUtc()
            .Should()
            .BeOnOrBefore(start.ToDateTimeUtc())
            .And
            .BeOnOrAfter(start.Minus(Duration.FromDays(1)).ToDateTimeUtc());
    }


    [Fact]
    public void Random_time_between_two_dates()
    {
        var start = new ZonedDateTime(Instant.FromUtc(2015, 6, 6, 4, 17, 41), DateTimeZone.Utc);
        var end = new ZonedDateTime(Instant.FromUtc(2015, 7, 6, 4, 17, 41), DateTimeZone.Utc);

        dataSet.Between(start, end).ToDateTimeUtc()
            .Should()
            .BeOnOrAfter(start.ToDateTimeUtc())
            .And
            .BeOnOrBefore(end.ToDateTimeUtc());

        //and reverse...
        dataSet.Between(end, start).ToDateTimeUtc()
            .Should()
            .BeOnOrAfter(start.ToDateTimeUtc())
            .And
            .BeOnOrBefore(end.ToDateTimeUtc());
    }

    [Fact]
    public void Time_that_will_happen_soon()
    {
        var start = SystemClock.Instance.GetCurrentInstant();
        dataSet.Soon(3).ToDateTimeUtc()
            .Should()
            .BeAfter(start.ToDateTimeUtc())
            .And.BeBefore(start.Plus(Duration.FromDays(3)).ToDateTimeUtc());
    }
}