## WordPress Sample

The goal of this sample is to fully compile an unmodified clone of WordPress into .NET/.NET Core. The project consists of several parts:

- `website` contains sources of WordPress that are compiled into a single .NET Core assembly (`website.dll`).
- `wwwroot` is the web content folder.
- `app` project is executable sample ASP.NET Core web server bootsrapping scripts from `website`.

## What does it do?

The PHP sources in `website` are compiled to .NET Core by Peachpie compiler, which is seamlessly downloaded by *dotnet* itself.

The sample instantiates Kestrel - the opensource web server - and the ASP.NET Core pipeline. The pipeline handles requests to PHP files using the Peachpie `RequestDelegate` by calling the corresponding compiled scripts in `website.dll`.

Note that the original PHP sources (*.php files) in `website` are not needed to run the compiled application.

## Prerequisites

- .NET Core 1.0 or newer
- Optionally - Visual Studio Code 

## How to run the project

1. `dotnet restore`
2. `dotnet run -p app`
