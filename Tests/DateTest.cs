using Bogus.NodaTime;
using FluentAssertions;
using NodaTime;
using Xunit;

public class InstantTest : SeededTest
{
    public InstantTest()
    {
        date = new InstantDataSet();
    }

    InstantDataSet date;

    [Fact]
    public void can_get_date_in_future()
    {
        var starting = Instant.FromUtc(2015,6,6,4,17,41);
        date.Future(reference: starting).Should()
            .BeLessOrEqualTo(starting.Plus(Duration.FromDays(100)))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void can_get_date_in_future_with_options()
    {
        var starting = Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        date.Future(reference: starting, daysToGoForward: 500).Should()
            .BeLessOrEqualTo(starting.Plus(Duration.FromDays(500)))
            .And
            .BeGreaterOrEqualTo(starting);
    }

    [Fact]
    public void can_get_date_in_past()
    {
        var starting = Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        date.Past(reference: starting).Should()
            .BeLessOrEqualTo(starting)
            .And
            .BeGreaterOrEqualTo(starting.Minus(Duration.FromDays(100)));
    }

    [Fact]
    public void can_get_date_in_past_0_days_results_in_random_time()
    {
        var now = SystemClock.Instance.GetCurrentInstant();
        date.Recent(0).Should()
            .BeLessOrEqualTo(now)
            .And
            .BeGreaterOrEqualTo(now.Minus(Duration.FromDays(1)));
    }

    [Fact]
    public void can_get_date_in_past_with_custom_options()
    {
        var starting = Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        date.Past(reference: starting, daysToGoBack: 500).Should()
            .BeLessOrEqualTo(starting)
            .And
            .BeGreaterOrEqualTo(starting.Minus(Duration.FromDays(500)));
    }

    [Fact]
    public void can_get_date_recently_within_the_year()
    {
        var start = SystemClock.Instance.GetCurrentInstant();
        date.Recent()
            .Should()
            .BeLessOrEqualTo(start)
            .And
            .BeGreaterOrEqualTo(start.Minus(Duration.FromDays(1)));
    }


    [Fact]
    public void can_get_random_time_between_two_dates()
    {
        var start= Instant.FromUtc(2015, 6, 6, 4, 17, 41);
        var end = Instant.FromUtc(2015, 7, 6, 4, 17, 41);

        date.Between(start, end)
            .Should()
            .BeGreaterOrEqualTo(start)
            .And
            .BeLessOrEqualTo(end);

        //and reverse...
        date.Between(end, start)
            .Should()
            .BeGreaterOrEqualTo(start)
            .And
            .BeLessOrEqualTo(end);
    }

    [Fact]
    public void get_a_date_time_that_will_happen_soon()
    {
        var start = SystemClock.Instance.GetCurrentInstant();
        date.Soon(3)
            .Should()
            .BeGreaterThan(start)
            .And.BeLessThan(start.Plus(Duration.FromDays(3)));
    }
}