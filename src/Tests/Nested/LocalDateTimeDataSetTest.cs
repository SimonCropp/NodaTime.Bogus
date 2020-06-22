using Bogus.NodaTime;
using FluentAssertions;
using NodaTime;
using Xunit;

public class LocalDateTimeDataSetTest :
    SeededTest
{
    public LocalDateTimeDataSetTest()
    {
        dataSet = new LocalDateTimeDataSet(()=> DateTimeZone.Utc);
    }

    LocalDateTimeDataSet dataSet;

    [Fact]
    public void Future()
    {
           var starting = new LocalDateTime(2015,6,6,4,17,41);
        dataSet.Future(reference: starting).Should()
            .BeLessOrEqualTo(starting.Plus(Period.FromDays(100)))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void Future_with_options()
    {
        var starting = new LocalDateTime(2015, 6, 6, 4, 17, 41);
        dataSet.Future(reference: starting, daysToGoForward: 500).Should()
            .BeLessOrEqualTo(starting.Plus(Period.FromDays(500)))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void Past()
    {
        var starting = new LocalDateTime(2015, 6, 6, 4, 17, 41);
        dataSet.Past(reference: starting).Should()
            .BeLessOrEqualTo(starting)
            .And
            .BeGreaterOrEqualTo(starting.Minus(Period.FromDays(100)));
    }

    [Fact]
    public void Past_0_days_results_in_Random_time()
    {
        var now = Now();
        dataSet.Past(0).Should()
            .BeLessOrEqualTo(now.Plus(Period.FromSeconds(1)))
            .And
            .BeGreaterOrEqualTo(now.Minus(Period.FromDays(1)));
    }

    [Fact]
    public void Past_with_custom_options()
    {
        var starting = new LocalDateTime(2015, 6, 6, 4, 17, 41);
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

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(500)]
    public void Recently_with_many_days(int days)
    {
        var start = Now();
        dataSet.Recent(days)
            .Should()
            .BeLessOrEqualTo(start)
            .And
            .BeGreaterOrEqualTo(start.Minus(Period.FromDays(days)));
    }


    [Fact]
    public void Random_time_between_two_dates()
    {
        var start= new LocalDateTime(2015, 6, 6, 4, 17, 41);
        var end = new LocalDateTime(2015, 6, 8, 4, 17, 41);

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
    public void Random_time_between_two_far_apart_dates()
    {
        var start = new LocalDateTime(1956, 6, 6, 4, 17, 41);
        var end = new LocalDateTime(2020, 7, 3, 2, 17, 41);

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

    LocalDateTime Now()
    {
        var currentInstant = SystemClock.Instance.GetCurrentInstant();
        return currentInstant.InUtc().LocalDateTime;
    }
}