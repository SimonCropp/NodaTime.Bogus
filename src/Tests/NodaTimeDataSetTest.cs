using Bogus.NodaTime;
using NodaTime;
using Xunit;

public class NodaTimeDataSetTest
{
    public NodaTimeDataSetTest()
    {
        dataSet = new NodaTimeDataSet(() => DateTimeZone.Utc);
    }

    NodaTimeDataSet dataSet;

    [Fact]
    public void Period()
    {
        var period = dataSet.Period();
        Assert.NotNull(period);
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
}