# Iso8601DurationHelper

[![NuGet version](https://img.shields.io/nuget/v/Iso8601DurationHelper.svg?logo=nuget)](https://www.nuget.org/packages/Iso8601DurationHelper)
[![AppVeyor build](https://img.shields.io/appveyor/ci/thomaslevesque/iso8601duration.svg?logo=appveyor)](https://ci.appveyor.com/project/thomaslevesque/iso8601duration)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/thomaslevesque/iso8601duration.svg?logo=appveyor)](https://ci.appveyor.com/project/thomaslevesque/iso8601duration/build/tests)

A small library to handle ISO8601 durations (e.g. `P1Y` for 1 year, `PT2H30M` for 2 hours and 30 minutes) in C#.

Some libraries attempt to parse these durations to `TimeSpan`, but it doesn't really make sense, because `TimeSpan` doesn't have a concept of month, so they just translate `P1M` to 30 days. This is wrong because all months don't have the same number of days; January 1 + 1 month should be February 1, not January 31.

This library introduces a `Duration` struct with operators to add a duration to or subtract a duration from a date, with the proper semantics. For instance:

```csharp
var startDate = new DateTime(2018, 1, 1);
var duration = Duration.Parse("P2M");
var endDate = startDate + duration; // 2018/03/01
```
