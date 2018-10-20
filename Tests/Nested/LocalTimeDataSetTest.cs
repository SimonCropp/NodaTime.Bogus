using Bogus.NodaTime;
using FluentAssertions;
using NodaTime;
using Xunit;

public class LocalTimeDataSetTest : SeededTest
{
    public LocalTimeDataSetTest()
    {
        dataSet = new LocalTimeDataSet(()=> DateTimeZone.Utc);
    }

    LocalTimeDataSet dataSet;

    [Fact]
    public void Future()
    {
           var starting = new LocalTime(4,17,41);
        dataSet.Future(reference: starting).Should()
            .BeLessOrEqualTo(starting.PlusHours(10))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void Future_with_options()
    {
        var starting = new LocalTime(4, 17, 41);
        dataSet.Future(reference: starting, hoursToGoForward: 2).Should()
            .BeLessOrEqualTo(starting.PlusHours(2))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void Past()
    {
        var starting = new LocalTime(14, 17, 41);
        dataSet.Past(reference: starting).Should()
            .BeLessOrEqualTo(starting)
            .And
            .BeGreaterOrEqualTo(starting.PlusHours(-10));
    }

    [Fact]
    public void Past_0_days_results_in_Random_time()
    {
        var now = Now();
        dataSet.Recent(0).Should()
            .BeLessOrEqualTo(now)
            .And
            .BeGreaterOrEqualTo(now.PlusMinutes(-59));
    }

    [Fact]
    public void Past_with_custom_options()
    {
        var starting = new LocalTime(4, 17, 41);
        dataSet.Past(reference: starting, hoursToGoBack: 1).Should()
            .BeLessOrEqualTo(starting)
            .And
            .BeGreaterOrEqualTo(starting.PlusHours(-1));
    }

    [Fact]
    public void Recently()
    {
        var start = Now();
        dataSet.Recent()
            .Should()
            .BeLessOrEqualTo(start)
            .And
            .BeGreaterOrEqualTo(start.PlusHours(-1));
    }


    [Fact]
    public void Random_time_between_two_dates()
    {
        var start= new LocalTime(4, 17, 41);
        var end = new LocalTime( 5, 17, 41);

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
        var start = Now();
        dataSet.Soon(2)
            .Should()
            .BeGreaterThan(start)
            .And.BeLessThan(start.PlusHours(2));
    }

    LocalTime Now()
    {
        var currentInstant = SystemClock.Instance.GetCurrentInstant();
        return currentInstant.InUtc().TimeOfDay;
    }
}