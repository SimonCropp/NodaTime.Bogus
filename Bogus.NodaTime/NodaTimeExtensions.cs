using Bogus.NodaTime;
using Bogus.Premium;

namespace Bogus
{
    public static class NodaTimeExtensions
    {
        public static NodaTimeDataSet NodaT(this Faker faker)
        {
            Guard.AgainstNull(faker, nameof(faker));
            var dataSet = ContextHelper.GetOrSet(faker, () => new NodaTimeDataSet());

            return dataSet;
        }
    }
}