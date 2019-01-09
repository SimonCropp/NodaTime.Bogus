using System;
using Bogus.NodaTime;
using NodaTime;
using Xunit;
using Xunit.Abstractions;

public class NodaTimeDataSetTest
{
    private readonly ITestOutputHelper output;

    public NodaTimeDataSetTest(ITestOutputHelper output)
    {
        this.output = output;
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
        output.WriteLine("AAAAA");
        Console.WriteLine("AAAAA");
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