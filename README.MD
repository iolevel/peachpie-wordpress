<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/WordPress_logo.svg/2000px-WordPress_logo.svg.png" width="400"/>

## WordPress Sample

The goal of this sample is to fully compile an unmodified clone of WordPress into .NET/.NET Core. The project consists of several parts:

- `website` contains sources of WordPress that are compiled into a single .NET Core assembly (`website.dll`).
- `wwwroot` is the web content folder.
- `app` project is executable sample ASP.NET Core web server bootstrapping scripts from `website`.

## What does this do?

This sample contains the complete source code of WordPress without any modifications. The purpose is for this code to be compiled by Peachpie, resulting in the output running on Microsoft .NET Core. 

Therefore, if everything works as it should, you will see the standard unchanged WordPress in the same way as you would in the traditional PHP version. The difference is that the compiled website runs on .NET Core in the background. 

## How does this work?

The PHP sources in `website` are compiled to .NET Core by Peachpie compiler, which is seamlessly downloaded by *dotnet* itself.

The sample instantiates Kestrel - the opensource web server - and the ASP.NET Core pipeline. The pipeline handles requests to PHP files using the Peachpie `RequestDelegate` by calling the corresponding compiled scripts in `website.dll`.

Note that the original PHP sources (*.php files) in `website` are not needed to run the compiled application.

## Prerequisites

- .NET Core 1.0 or newer
- Optionally - Visual Studio Code using the official Peachpie for VSCode extension available on the [Marketplace](https://marketplace.visualstudio.com/items?itemName=iolevel.peachpie-vscode)

## How to run the project

1. `dotnet restore`
2. `dotnet run -p app`
