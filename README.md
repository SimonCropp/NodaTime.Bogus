# NodaTime.Bogus

Add support for [NodaTime](https://nodatime.org/) to [Bogus](https://github.com/bchavez/Bogus).

**This project is supported by the community via [Patreon sponsorship](https://www.patreon.com/join/simoncropp). If you are using this project to deliver business value or build commercial software it is expected that you will provide support [via Patreon](https://www.patreon.com/join/simoncropp). If you do not feel this project is worth supporting, to ensure its long term existence, perhaps you should find an alternative tool or build the functionality yourself.**


## NuGet [![NuGet Status](http://img.shields.io/nuget/v/NodaTime.Bogus.svg?style=flat)](https://www.nuget.org/packages/NodaTime.Bogus/)

https://nuget.org/packages/NodaTime.Bogus/

    PM> Install-Package NodaTime.Bogus


## Usage


This project extends `Faker` with `.Noda()`.

```csharp
var faker = new Faker<Target>()
    .RuleFor(u => u.Property1, (f, u) => f.Noda().Duration())
    .RuleFor(u => u.Property2, (f, u) => f.Noda().Instant.Recent())
    .RuleFor(u => u.Property3, (f, u) => f.Noda().ZonedDateTime.Future());
```

There are several top level generators:

 * `CalendarSystem()`: Creates a random [CalendarSystem](https://nodatime.org/unstable/api/NodaTime.CalendarSystem.html).
 * `DateTimeZone()`: Creates a random [DateTimeZone](https://nodatime.org/unstable/api/NodaTime.DateTimeZone.html).
 * `Duration()`: Creates a random [Duration](https://nodatime.org/unstable/api/NodaTime.Duration.html).
 * `IsoDayOfWeek()`:  Creates a random [IsoDayOfWeek](https://nodatime.org/unstable/api/NodaTime.IsoDayOfWeek.html).
 * `Offset()`:  Creates a random [Offset](https://nodatime.org/unstable/api/NodaTime.Offset.html).
 * `Period()`:  Creates a random [Period](https://nodatime.org/unstable/api/NodaTime.Period.html).
 * `PeriodUnits()`:  Creates a random [PeriodUnits](https://nodatime.org/unstable/api/NodaTime.PeriodUnits.html).

There are several nested generators that provide `Past`, `Soon`, `Future`, `Between`, and `Recent` semantics:

 * `.Instant`: Generators for [Instant](https://nodatime.org/unstable/api/NodaTime.Instant.html)
 * `.LocalDate`: Generators for [LocalDate](https://nodatime.org/unstable/api/NodaTime.LocalDate.html)
 * `.LocalDateTime`: Generators for [LocalDateTime](https://nodatime.org/unstable/api/NodaTime.LocalDateTime.html)
 * `.LocalTime`: Generators for [LocalTime](https://nodatime.org/unstable/api/NodaTime.LocalTime.html)
 * `.ZonedDateTime`: Generators for [ZonedDateTime](https://nodatime.org/unstable/api/NodaTime.ZonedDateTime.html)


## Icon

<a href="https://thenounproject.com/term/calendar/689871/" target="_blank">Calendar</a> designed by <a href="https://thenounproject.com/monstercritic/" target="_blank">Monster Critic</a> from The Noun Project