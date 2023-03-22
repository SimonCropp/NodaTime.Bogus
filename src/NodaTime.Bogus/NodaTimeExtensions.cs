using Bogus.NodaTime;
using Bogus.Premium;
using NodaTime;

namespace Bogus;

public static class NodaTimeExtensions
{
    public static NodaTimeDataSet Noda(this Faker faker) =>
        Noda(faker, () => DateTimeZone.Utc);

    public static NodaTimeDataSet Noda(this Faker faker, Func<DateTimeZone> dateTimeZoneBuilder) =>
        ContextHelper.GetOrSet(faker, () => new NodaTimeDataSet(dateTimeZoneBuilder));
}