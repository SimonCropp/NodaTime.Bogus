using Bogus.NodaTime;
using Bogus.Premium;
using NodaTime;

namespace Bogus
{
    public static class NodaTimeExtensions
    {
        public static NodaTimeDataSet Noda(this Faker faker)
        {
            Guard.AgainstNull(faker, nameof(faker));
            return ContextHelper.GetOrSet(faker, () =>
            {
                var dataSet = new NodaTimeDataSet(()=> DateTimeZone.Utc);
                return dataSet;
            });
        }
    }
}