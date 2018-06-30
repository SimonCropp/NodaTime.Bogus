using System.Diagnostics;
using Bogus;
using NodaTime;
using Xunit;
using Xunit.Abstractions;

public class FakerUsage
{
    ITestOutputHelper output;

    public FakerUsage(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void Run()
    {
        var faker = new Faker<Target>()
            .RuleFor(u => u.Property1, (f, u) => f.Noda().Duration())
            .RuleFor(u => u.Property2, (f, u) => f.Noda().Instant.Recent())
            .RuleFor(u => u.Property3, (f, u) => f.Noda().ZonedDateTime.Future());

        var target = faker.Generate();
        Debug.WriteLine(target.Property1);
        Debug.WriteLine(target.Property2);
        Debug.WriteLine(target.Property3);
    }

    public class Target
    {
        public Duration Property1 { get; set; }
        public Instant Property2 { get; set; }
        public ZonedDateTime Property3 { get; set; }
    }
}