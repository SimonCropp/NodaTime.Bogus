using Bogus.NodaTime;
using NodaTime;
using Xunit;

public class InstantDataSetTest : SeededTest
{
    public InstantDataSetTest() => dataSet = new();

    InstantDataSet dataSet;

    [Fact]
    public void Future()
    {
        var starting = Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        var result = dataSet.Future(reference: starting);
        Assert.True(result <= starting.Plus(Duration.FromDays(100)));
        Assert.True(result >= starting);
    }

    [Fact]
    public void Future_with_options()
    {
        var starting = Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        var result = dataSet.Future(reference: starting, daysToGoForward: 500);
        Assert.True(result <= starting.Plus(Duration.FromDays(500)));
        Assert.True(result >= starting);
    }

    [Fact]
    public void Past()
    {
        var starting = Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        var result = dataSet.Past(reference: starting);
        Assert.True(result <= starting);
        Assert.True(result >= starting.Minus(Duration.FromDays(100)));
    }

    [Fact]
    public void Past_0_days_results_in_Random_time()
    {
        var instant = dataSet.Past(0);
        var now = SystemClock.Instance.GetCurrentInstant();
        Assert.True(instant <= now);
        Assert.True(instant >= now.Minus(Duration.FromDays(1)));
    }

    [Fact]
    public void Past_with_custom_options()
    {
        var starting = Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        var result = dataSet.Past(reference: starting, daysToGoBack: 500);
        Assert.True(result <= starting);
        Assert.True(result >= starting.Minus(Duration.FromDays(500)));
    }

    [Fact]
    public void Recently()
    {
        var start = SystemClock.Instance.GetCurrentInstant();
        var result = dataSet.Recent();
        Assert.True(result <= start);
        Assert.True(result >= start.Minus(Duration.FromDays(1)));
    }

    [Fact]
    public void Random_time_between_two_dates()
    {
        var start = Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        var end = Instant.FromUtc(2015, 7, 6, 4, 17, 41);

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
        var start = SystemClock.Instance.GetCurrentInstant();
        var result = dataSet.Soon(3);
        Assert.True(result > start);
        Assert.True(result < start.Plus(Duration.FromDays(3)));
    }
}