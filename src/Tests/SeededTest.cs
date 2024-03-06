using Bogus;

[Collection("Seeded Test")]
public class SeededTest
{
    protected static void ResetGlobalSeed() =>
        Randomizer.Seed = new(3116);

    public SeededTest() =>
        //set the random gen manually to a seeded value
        ResetGlobalSeed();
}