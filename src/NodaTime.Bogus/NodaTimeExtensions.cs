using Bogus.NodaTime;
using Bogus.Premium;
using NodaTime;

namespace Bogus;

public static class NodaTimeExtensions
{
    public static NodaTimeDataSet Noda(this Faker faker)
    {
        return Noda(faker, () => DateTimeZone.Utc);
    }

    public static NodaTimeDataSet Noda(this Faker faker, Func<DateTimeZone> dateTimeZoneBuilder)
    {
        return ContextHelper.GetOrSet(faker, () =>
        {
            var dataSet = new NodaTimeDataSet(dateTimeZoneBuilder);
            return dataSet;
        });
    }
}