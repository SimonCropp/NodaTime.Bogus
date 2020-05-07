using Bogus.NodaTime;
using FluentAssertions;
using NodaTime;
using Xunit;
using Xunit.Abstractions;

public class LocalDateDataSetTest :
    SeededTest
{
    public LocalDateDataSetTest(ITestOutputHelper output) :
        base(output)
    {
        dataSet = new LocalDateDataSet(()=> DateTimeZone.Utc);
    }

    LocalDateDataSet dataSet;

    [Fact]
    public void Future()
    {
           var starting = new LocalDate(2015,6,6);
        dataSet.Future(reference: starting).Should()
            .BeLessOrEqualTo(starting.Plus(Period.FromDays(100)))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void Future_with_options()
    {
        var starting = new LocalDate(2015, 6, 6);
        dataSet.Future(reference: starting, daysToGoForward: 500).Should()
            .BeLessOrEqualTo(starting.Plus(Period.FromDays(500)))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void Past()
    {
        var starting = new LocalDate(2015, 6, 6);
        dataSet.Past(reference: starting).Should()
            .BeLessOrEqualTo(starting)
            .And
            .BeGreaterOrEqualTo(starting.Minus(Period.FromDays(100)));
    }

    [Fact]
    public void Past_0_days_results_in_Random_time()
    {
        var now = Now();
        dataSet.Recent(0).Should()
            .BeLessOrEqualTo(now)
            .And
            .BeGreaterOrEqualTo(now.Minus(Period.FromDays(1)));
    }

    [Fact]
    public void Past_with_custom_options()
    {
        var starting = new LocalDate(2015, 6, 6);
        dataSet.Past(reference: starting, daysToGoBack: 500).Should()
            .BeLessOrEqualTo(starting)
            .And
            .BeGreaterOrEqualTo(starting.Minus(Period.FromDays(500)));
    }

    [Fact]
    public void Recently()
    {
        var start = Now();
        dataSet.Recent()
            .Should()
            .BeLessOrEqualTo(start)
            .And
            .BeGreaterOrEqualTo(start.Minus(Period.FromDays(10)));
    }


    [Fact]
    public void Random_time_between_two_dates()
    {
        var start= new LocalDate(2015, 6, 6);
        var end = new LocalDate(2015, 6, 8);

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
    public void Random_time_between_far_apart_dates()
    {
        var start = new LocalDate(1976, 6, 6);
        var end = new LocalDate(2020, 2, 8);

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
        dataSet.Soon(3)
            .Should()
            .BeGreaterThan(start)
            .And.BeLessThan(start.Plus(Period.FromDays(3)));
    }

    LocalDate Now()
    {
        var currentInstant = SystemClock.Instance.GetCurrentInstant();
        return currentInstant.InUtc().Date;
    }
}