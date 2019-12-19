# <img src="/src/icon.png" height="30px"> NodaTime.Bogus

[![Build status](https://ci.appveyor.com/api/projects/status/5if48t86ivcnrits/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/NodaTime-Bogus)
[![NuGet Status](https://img.shields.io/nuget/v/NodaTime.Bogus.svg)](https://www.nuget.org/packages/NodaTime.Bogus/)

Add support for [NodaTime](https://nodatime.org/) to [Bogus](https://github.com/bchavez/Bogus).


## NuGet

https://nuget.org/packages/NodaTime.Bogus/


## Usage

This project extends `Faker` with `.Noda()`.

snippet: usage

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


## Release Notes

See [closed milestones](../../milestones?state=closed).


## Icon

[Calendar](https://thenounproject.com/term/calendar/689871/) designed by [Monster Critic](https://thenounproject.com/monstercritic/) from [The Noun Project](https://thenounproject.com/monstercritic/).