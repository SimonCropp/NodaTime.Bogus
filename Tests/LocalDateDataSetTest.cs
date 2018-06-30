using Bogus.NodaTime;
using FluentAssertions;
using NodaTime;
using Xunit;

public class LocalDateDataSetTest : SeededTest
{
    public LocalDateDataSetTest()
    {
        dataSet = new LocalDateDataSet(()=> DateTimeZone.Utc);
    }

    LocalDateDataSet dataSet;

    [Fact]
    public void can_get_date_in_future()
    {
           var starting = new LocalDate(2015,6,6);
        dataSet.Future(reference: starting).Should()
            .BeLessOrEqualTo(starting.Plus(Period.FromDays(100)))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void can_get_date_in_future_with_options()
    {
        var starting = new LocalDate(2015, 6, 6);
        dataSet.Future(reference: starting, daysToGoForward: 500).Should()
            .BeLessOrEqualTo(starting.Plus(Period.FromDays(500)))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void can_get_date_in_past()
    {
        var starting = new LocalDate(2015, 6, 6);
        dataSet.Past(reference: starting).Should()
            .BeLessOrEqualTo(starting)
            .And
            .BeGreaterOrEqualTo(starting.Minus(Period.FromDays(100)));
    }

    [Fact]
    public void can_get_date_in_past_0_days_results_in_random_time()
    {
        var now = Now();
        dataSet.Recent(0).Should()
            .BeLessOrEqualTo(now)
            .And
            .BeGreaterOrEqualTo(now.Minus(Period.FromDays(1)));
    }

    [Fact]
    public void can_get_date_in_past_with_custom_options()
    {
        var starting = new LocalDate(2015, 6, 6);
        dataSet.Past(reference: starting, daysToGoBack: 500).Should()
            .BeLessOrEqualTo(starting)
            .And
            .BeGreaterOrEqualTo(starting.Minus(Period.FromDays(500)));
    }

    [Fact]
    public void can_get_date_recently()
    {
        var start = Now();
        dataSet.Recent()
            .Should()
            .BeLessOrEqualTo(start)
            .And
            .BeGreaterOrEqualTo(start.Minus(Period.FromDays(10)));
    }


    [Fact]
    public void can_get_random_time_between_two_dates()
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
    public void get_a_date_time_that_will_happen_soon()
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