# <img src="/src/icon.png" height="30px"> NodaTime.Bogus

[![Build status](https://ci.appveyor.com/api/projects/status/5if48t86ivcnrits/branch/main?svg=true)](https://ci.appveyor.com/project/SimonCropp/NodaTime-Bogus)
[![NuGet Status](https://img.shields.io/nuget/v/NodaTime.Bogus.svg)](https://www.nuget.org/packages/NodaTime.Bogus/)

Add support for [NodaTime](https://nodatime.org/) to [Bogus](https://github.com/bchavez/Bogus).

**See [Milestones](../../milestones?state=closed) for release notes.**


## NuGet package

https://nuget.org/packages/NodaTime.Bogus/


## Usage

This project extends `Faker` with `.Noda()`.

<!-- snippet: usage -->
<a id='snippet-usage'></a>
```cs
var faker = new Faker<Target>()
    .RuleFor(u => u.Property1, (f, _) => f.Noda().Duration())
    .RuleFor(u => u.Property2, (f, _) => f.Noda().Instant.Recent())
    .RuleFor(u => u.Property3, (f, _) => f.Noda().ZonedDateTime.Future());

var target = faker.Generate();
Debug.WriteLine(target.Property1);
Debug.WriteLine(target.Property2);
Debug.WriteLine(target.Property3);
```
<sup><a href='/src/Tests/FakerUsage.cs#L9-L19' title='Snippet source file'>snippet source</a> | <a href='#snippet-usage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

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

[Calendar](https://thenounproject.com/term/calendar/689871/) designed by [Monster Critic](https://thenounproject.com/monstercritic/) from [The Noun Project](https://thenounproject.com/monstercritic/).
