using Bogus.NodaTime;
using FluentAssertions;
using NodaTime;

public class InstantDataSetTest :
    SeededTest
{
    public InstantDataSetTest() =>
        dataSet = new();

    InstantDataSet dataSet;

    [Fact]
    public void Future()
    {
        var starting = Instant.FromUtc(2015,6,6,4,17,41);
        dataSet.Future(reference: starting).Should()
            .BeLessOrEqualTo(starting.Plus(Duration.FromDays(100)))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void Future_with_options()
    {
        var starting = Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        dataSet.Future(reference: starting, daysToGoForward: 500).Should()
            .BeLessOrEqualTo(starting.Plus(Duration.FromDays(500)))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void Past()
    {
        var starting = Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        dataSet.Past(reference: starting).Should()
            .BeLessOrEqualTo(starting)
            .And
            .BeGreaterOrEqualTo(starting.Minus(Duration.FromDays(100)));
    }

    [Fact]
    public void Past_0_days_results_in_Random_time()
    {
        var instant = dataSet.Past(0);
        var now = SystemClock.Instance.GetCurrentInstant();
        instant.Should()
            .BeLessOrEqualTo(now)
            .And
            .BeGreaterOrEqualTo(now.Minus(Duration.FromDays(1)));
    }

    [Fact]
    public void Past_with_custom_options()
    {
        var starting = Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        dataSet.Past(reference: starting, daysToGoBack: 500).Should()
            .BeLessOrEqualTo(starting)
            .And
            .BeGreaterOrEqualTo(starting.Minus(Duration.FromDays(500)));
    }

    [Fact]
    public void Recently()
    {
        var start = SystemClock.Instance.GetCurrentInstant();
        dataSet.Recent()
            .Should()
            .BeLessOrEqualTo(start)
            .And
            .BeGreaterOrEqualTo(start.Minus(Duration.FromDays(1)));
    }


    [Fact]
    public void Random_time_between_two_dates()
    {
        var start= Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        var end = Instant.FromUtc(2015, 7, 6, 4, 17, 41);

        dataSet.Between(start, end)
            .Should()
            .BeGreaterOrEqualTo(start)
            .And
            .BeLessOrEqualTo(end);

        //and reverse...
        dataSet.Between(end, start)
            .Should()
            .BeGreaterOrEqualTo(start)
            .And
            .BeLessOrEqualTo(end);
    }

    [Fact]
    public void Time_that_will_happen_soon()
    {
        var start = SystemClock.Instance.GetCurrentInstant();
        dataSet.Soon(3)
            .Should()
            .BeGreaterThan(start)
            .And.BeLessThan(start.Plus(Duration.FromDays(3)));
    }
}