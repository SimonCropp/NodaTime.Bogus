using FluentAssertions;

public class NodaTimeDataSetTest
{
    NodaTimeDataSet dataSet = new(() => DateTimeZone.Utc);

    [Fact]
    public void Period()
    {
        var period = dataSet.Period();
        Assert.NotNull(period);
    }

    [Fact]
    public void Period_with_large_max_period()
    {
        var period = dataSet.Period(NodaTime.Period.FromDays(1000));
        Assert.NotNull(period);
    }

    [Fact]
    public void Period_with_units()
    {
        var period = dataSet.Period(NodaTime.Period.FromMonths(2), units: PeriodUnits.AllUnits);
        Assert.NotNull(period);
    }

    [Fact]
    public void Period_with_anchor_date()
    {
        var anchorDateTime = new LocalDateTime(2013, 1, 3, 4, 6, 7);
        var period = dataSet.Period(NodaTime.Period.FromMonths(2), anchorDateTime);

        (anchorDateTime + period).Should().BeLessOrEqualTo(anchorDateTime + NodaTime.Period.FromMonths(2));
    }

    [Fact]
    public void Duration()
    {
        var duration = dataSet.Duration();
        Assert.NotEqual(default, duration);
    }

    [Fact]
    public void CalendarSystem()
    {
        var calendarSystem = dataSet.CalendarSystem();
        Assert.NotNull(calendarSystem);
    }

    [Fact]
    public void Random_changed()
    {
        var random = new Bogus.Randomizer();
        var sut = new NodaTimeDataSet
        {
            Random = random
        };

        sut.Instant.Random.Should().BeSameAs(random);
        sut.LocalDate.Random.Should().BeSameAs(random);
        sut.LocalDateTime.Random.Should().BeSameAs(random);
        sut.LocalTime.Random.Should().BeSameAs(random);
        sut.ZonedDateTime.Random.Should().BeSameAs(random);
    }
}
