public class ZonedDateTimeDataSetTest :
    SeededTest
{
    ZonedDateTimeDataSet dataSet = new(() => DateTimeZone.Utc);

    [Fact]
    public void Future()
    {
        var starting = new ZonedDateTime(Instant.FromUtc(2015, 6, 6, 4, 17, 41), DateTimeZone.Utc);
        var zonedDateTime = dataSet.Future(reference: starting).ToDateTimeUtc();
        Assert.True(zonedDateTime <= starting.Plus(Duration.FromDays(100)).ToDateTimeUtc());
        Assert.True(zonedDateTime >= starting.ToDateTimeUtc());
    }

    [Fact]
    public void Future_with_options()
    {
        var starting = new ZonedDateTime(Instant.FromUtc(2015, 6, 6, 4, 17, 41), DateTimeZone.Utc);
        var result = dataSet.Future(reference: starting, daysToGoForward: 500).ToDateTimeUtc();
        Assert.True(result <= starting.Plus(Duration.FromDays(500)).ToDateTimeUtc());
        Assert.True(result >= starting.ToDateTimeUtc());
    }

    [Fact]
    public void Past()
    {
        var starting = new ZonedDateTime(Instant.FromUtc(2015, 6, 6, 4, 17, 41), DateTimeZone.Utc);
        var result = dataSet.Past(reference: starting).ToDateTimeUtc();
        Assert.True(result <= starting.ToDateTimeUtc());
        Assert.True(result >= starting.Minus(Duration.FromDays(100)).ToDateTimeUtc());
    }

    [Fact]
    public void Past_0_days_results_in_Random_time()
    {
        var now = SystemClock.Instance.GetCurrentInstant();
        var result = dataSet.Recent(0).ToDateTimeUtc();
        Assert.True(result <= DateTime.UtcNow);
        Assert.True(result >= now.Minus(Duration.FromDays(1)).ToDateTimeUtc());
    }

    [Fact]
    public void Past_with_custom_options()
    {
        var starting = new ZonedDateTime(Instant.FromUtc(2015, 6, 6, 4, 17, 41), DateTimeZone.Utc);
        var result = dataSet.Past(reference: starting, daysToGoBack: 500).ToDateTimeUtc();
        Assert.True(result <= starting.ToDateTimeUtc());
        Assert.True(result >= starting.Minus(Duration.FromDays(500)).ToDateTimeUtc());
    }

    [Fact]
    public void Recently()
    {
        var start = SystemClock.Instance.GetCurrentInstant();
        var result = dataSet.Recent().ToDateTimeUtc();
        Assert.True(result <= start.ToDateTimeUtc());
        Assert.True(result >= start.Minus(Duration.FromDays(1)).ToDateTimeUtc());
    }

    [Fact]
    public void Random_time_between_two_dates()
    {
        var start = new ZonedDateTime(Instant.FromUtc(2015, 6, 6, 4, 17, 41), DateTimeZone.Utc);
        var end = new ZonedDateTime(Instant.FromUtc(2015, 7, 6, 4, 17, 41), DateTimeZone.Utc);

        var result = dataSet.Between(start, end).ToDateTimeUtc();
        Assert.True(result >= start.ToDateTimeUtc());
        Assert.True(result <= end.ToDateTimeUtc());

        // and reverse...
        result = dataSet.Between(end, start).ToDateTimeUtc();
        Assert.True(result >= start.ToDateTimeUtc());
        Assert.True(result <= end.ToDateTimeUtc());
    }

    [Fact]
    public void Time_that_will_happen_soon()
    {
        var start = SystemClock.Instance.GetCurrentInstant();
        var result = dataSet.Soon(3).ToDateTimeUtc();
        Assert.True(result > start.ToDateTimeUtc());
        Assert.True(result < start.Plus(Duration.FromDays(3)).ToDateTimeUtc());
    }
}