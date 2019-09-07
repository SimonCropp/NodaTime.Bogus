using Bogus.NodaTime;
using NodaTime;
using Xunit;
using Xunit.Abstractions;

public class NodaTimeDataSetTest :
    XunitApprovalBase
{
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

    public NodaTimeDataSetTest(ITestOutputHelper output) : base(output)
    {
        dataSet = new NodaTimeDataSet(() => DateTimeZone.Utc);
    }
}