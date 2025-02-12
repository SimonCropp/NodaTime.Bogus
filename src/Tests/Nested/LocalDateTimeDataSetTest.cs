public class LocalDateTimeDataSetTest :
    SeededTest
{
    LocalDateTimeDataSet dataSet = new(() => DateTimeZone.Utc);

    [Fact]
    public void Future()
    {
        var starting = new LocalDateTime(2015, 6, 6, 4, 17, 41);
        var result = dataSet.Future(reference: starting);
        Assert.True(result <= starting.Plus(Period.FromDays(100)));
        Assert.True(result >= starting);
    }

    [Fact]
    public void Future_with_options()
    {
        var starting = new LocalDateTime(2015, 6, 6, 4, 17, 41);
        var result = dataSet.Future(reference: starting, daysToGoForward: 500);
        Assert.True(result <= starting.Plus(Period.FromDays(500)));
        Assert.True(result >= starting);
    }

    [Fact]
    public void Past()
    {
        var starting = new LocalDateTime(2015, 6, 6, 4, 17, 41);
        var result = dataSet.Past(reference: starting);
        Assert.True(result <= starting);
        Assert.True(result >= starting.Minus(Period.FromDays(100)));
    }

    [Fact]
    public void Past_0_days_results_in_Random_time()
    {
        var now = Now();
        var result = dataSet.Past(0);
        Assert.True(result <= now.Plus(Period.FromSeconds(1)));
        Assert.True(result >= now.Minus(Period.FromDays(1)));
    }

    [Fact]
    public void Past_with_custom_options()
    {
        var starting = new LocalDateTime(2015, 6, 6, 4, 17, 41);
        var result = dataSet.Past(reference: starting, daysToGoBack: 500);
        Assert.True(result <= starting);
        Assert.True(result >= starting.Minus(Period.FromDays(500)));
    }

    [Fact]
    public void Recently()
    {
        var start = Now();
        var result = dataSet.Recent();
        Assert.True(result <= start);
        Assert.True(result >= start.Minus(Period.FromDays(10)));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(500)]
    public void Recently_with_many_days(int days)
    {
        var start = Now();
        var result = dataSet.Recent(days);
        Assert.True(result <= start);
        Assert.True(result >= start.Minus(Period.FromDays(days)));
    }

    [Fact]
    public void Random_time_between_two_dates()
    {
        var start = new LocalDateTime(2015, 6, 6, 4, 17, 41);
        var end = new LocalDateTime(2015, 6, 8, 4, 17, 41);

        var result = dataSet.Between(start, end);
        Assert.True(result >= start);
        Assert.True(result <= end);

        // and reverse...
        result = dataSet.Between(end, start);
        Assert.True(result >= start);
        Assert.True(result <= end);
    }

    [Fact]
    public void Random_time_between_two_far_apart_dates()
    {
        var start = new LocalDateTime(1956, 6, 6, 4, 17, 41);
        var end = new LocalDateTime(2020, 7, 3, 2, 17, 41);

        var result = dataSet.Between(start, end);
        Assert.True(result >= start);
        Assert.True(result <= end);

        // and reverse...
        result = dataSet.Between(end, start);
        Assert.True(result >= start);
        Assert.True(result <= end);
    }

    [Fact]
    public void Time_that_will_happen_soon()
    {
        var start = Now();
        var result = dataSet.Soon(3);
        Assert.True(result > start);
        Assert.True(result < start.Plus(Period.FromDays(3)));
    }

    static LocalDateTime Now()
    {
        var currentInstant = SystemClock.Instance.GetCurrentInstant();
        return currentInstant.InUtc().LocalDateTime;
    }
}