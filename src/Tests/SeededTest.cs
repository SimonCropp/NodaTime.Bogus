using Bogus;
using Xunit;

[Collection("Seeded Test")]
public class SeededTest
{
    protected static void ResetGlobalSeed()
    {
        Randomizer.Seed = new(3116);
    }

    protected IEnumerable<T> Make<T>(int times, Func<T> a)
    {
        return Enumerable.Range(0, times)
            .Select(_ => a()).ToArray();
    }

    public SeededTest()
    {
        //set the random gen manually to a seeded value
        ResetGlobalSeed();
    }
}