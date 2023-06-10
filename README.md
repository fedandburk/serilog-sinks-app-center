# App Center sink for Serilog
![GitHub](https://img.shields.io/github/license/fedandburk/serilog-sinks-app-center.svg)
![Nuget](https://img.shields.io/nuget/v/fedandburk/serilog-sinks-app-center.svg)
[![CI](https://github.com/fedandburk/serilog-sinks-app-center/actions/workflows/ci.yml/badge.svg)](https://github.com/fedandburk/serilog-sinks-app-center/actions/workflows/ci.yml)
[![CD](https://github.com/fedandburk/serilog-sinks-app-center/actions/workflows/cd.yml/badge.svg)](https://github.com/fedandburk/serilog-sinks-app-center/actions/workflows/cd.yml)
[![CodeFactor](https://www.codefactor.io/repository/github/fedandburk/serilog-sinks-app-center/badge)](https://www.codefactor.io/repository/github/fedandburk/serilog-sinks-app-center/badge)

[Serilog](https://github.com/serilog/serilog) sink that uses [AppCenter.Analytics](https://docs.microsoft.com/en-us/appcenter/analytics/) and [AppCenter.Crashes](https://docs.microsoft.com/en-us/appcenter/sdk/crashes/xamarin) to log events.

App Center Crashes will receive events that include an `Exception`, while App Center Analytics will receive events that do not contain an `Exception`.

## Installation

Use [NuGet](https://www.nuget.org) package manager to install this library.

```bash
Install-Package Fedandburk.Serilog.Sinks.AppCenter
```

## Usage
```cs
using Fedandburk.Serilog.Sinks.AppCenter;

// Configure AppCenter first.
AppCenter.Start("AppCenterApiKey", typeof(Analytics), typeof(Crashes));

// Create logger that logs informational (and above) events to AppCenter.
var logger = new LoggerConfiguration()
    .WriteTo.AppCenter(LogEventLevel.Information)
    .CreateLogger();
```
