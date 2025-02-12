public class LocalTimeDataSetTest :
    SeededTest
{
    LocalTimeDataSet dataSet = new(() => DateTimeZone.Utc);

    [Fact]
    public void Future()
    {
        var starting = new LocalTime(4, 17, 41);
        var result = dataSet.Future(reference: starting);
        Assert.True(result <= starting.PlusMinutes(10));
        Assert.True(result >= starting);
    }

    [Fact]
    public void Future_with_options()
    {
        var starting = new LocalTime(4, 17, 41);
        var result = dataSet.Future(reference: starting, minutesToGoForward: 2);
        Assert.True(result <= starting.PlusMinutes(2));
        Assert.True(result >= starting);
    }

    [Fact]
    public void Past()
    {
        var starting = new LocalTime(14, 17, 41);
        var result = dataSet.Past(reference: starting);
        Assert.True(result <= starting);
        Assert.True(result >= starting.PlusMinutes(-10));
    }

    [Fact]
    public void Past_0_minutes_results_in_Random_time()
    {
        var localTime = dataSet.Past(0);
        var now = dataSet.Now();
        Assert.True(localTime <= now);
    }

    [Fact]
    public void Past_with_custom_options()
    {
        var starting = new LocalTime(4, 17, 41);
        var result = dataSet.Past(reference: starting, minutesToGoBack: 1);
        Assert.True(result <= starting);
        Assert.True(result >= starting.PlusMinutes(-1));
    }

    [Fact]
    public void Recently()
    {
        var localTime = dataSet.Recent();
        var start = Now();
        Assert.True(localTime <= start);
        Assert.True(localTime >= start.PlusMinutes(-1));
    }

    [Fact]
    public void Random_time_between_two_dates()
    {
        var start = new LocalTime(4, 17, 41);
        var end = new LocalTime(5, 17, 41);
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
        var result = dataSet.Soon(2);
        Assert.True(result > start);
        Assert.True(result < start.PlusMinutes(2));
    }

    static LocalTime Now()
    {
        var currentInstant = SystemClock.Instance.GetCurrentInstant();
        return currentInstant.InUtc().TimeOfDay;
    }
}