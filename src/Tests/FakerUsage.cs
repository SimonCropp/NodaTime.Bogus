using Bogus;

public class FakerUsage
{
    [Fact]
    public void Run()
    {
        #region usage
        var faker = new Faker<Target>()
            .RuleFor(u => u.Property1, (f, _) => f.Noda().Duration())
            .RuleFor(u => u.Property2, (f, _) => f.Noda().Instant.Recent())
            .RuleFor(u => u.Property3, (f, _) => f.Noda().ZonedDateTime.Future());

        var target = faker.Generate();
        Debug.WriteLine(target.Property1);
        Debug.WriteLine(target.Property2);
        Debug.WriteLine(target.Property3);
        #endregion
    }

    public class Target
    {
        public Duration Property1 { get; set; }
        public Instant Property2 { get; set; }
        public ZonedDateTime Property3 { get; set; }
    }
}