Chat with the community on Gitter if you need help:  
  <a href="https://gitter.im/iolevel/peachpie"><img src="https://badges.gitter.im/iolevel/peachpie.svg"></a>

This project demonstrates how to start an ASP.NET Core server, integrate WordPress into its pipeline and add additional PHP plugins into it.

# WordPress

Even though the project is noticeably small, it contains the entire WordPress CMS. How? This complex PHP web framework was already compiled into the .NET Standard library (see [wpdotnet-sdk](https://github.com/iolevel/wpdotnet-sdk) for more information) and packed into a NuGet package. This demo may just reference it.

To find out more about PeachPie, check out our [peachpie.io](https://www.peachpie.io/), which includes tutorials, benchmarks and articles on PeachPie.

## Create MySQL database

WordPress requires you to start your database server with a database in it. By default it expects a database server on localhost, port 3306, with a database named *"wordpress"*.

You can use *docker* to quickly start a database server in virtual environment:
```
docker run -e MYSQL_ROOT_PASSWORD=password -e MYSQL_DATABASE=wordpress -p 3306:3306 -d mysql
```

## Build &amp; Run

Note, this is a regular ASP.NET web project. Building, running, debugging, and deploying the site take advantage of `dotnet`, optionally Visual Studio.

```
dotnet run
```

# Configuration

The configuration itself serves as a demonstration of combining a legacy PHP application with .NET. The demo takes advantage of the ASP.NET Core configuration mechanism, namely the files `appsettings.json`, `appsettings.deployment.json` and `appsettings.production.json`. This allows for having a different configuration for each environment without the need of altering the source code of the project.

More on configuring WpDotNet on https://docs.peachpie.io/scenarios/wordpress/configuration/.

# WordPress PHP Plugin

With this approach it is possible to have a project containing only the plugin. WordPress itself is already compiled and the plugin has a dependency on it (like any other C# app you would make).

The build process will do following:
- compiles `.php` files within the `MyContent` directory. The content is implicitly nested within `wp-content` subdirectory.
- packs the compiled DLL together with content files (like images, scripts etc.) into a NuGet package.
- copies the project content into the application output directory, together with WordPress content files extracted from its NuGet.

# WordPress C# plugin

The project takes advantage of the `WpDotNet SDK` which provides a C# abstraction `IWpPlugin`. Objects of this type can be passed to the WordPress middleware (see `UseWordPress`) and hook onto filters and actions available in the WordPress API.

# Sourceless Distribution

The WordPress package was already configured, so the original PHP source code is not a part of the NuGet nor of the compiled DLL. In case of additional plugins (see `PeachPied.WordPress.MyPlugin`), it depends what source files are needed in run time. Usually only the main plugin file (containing the WordPress metadata) has to be distributed together with the DLL, so that WordPress can read its content and work properly. The other files can be excluded from the package.

As a result, this project serves as a demonstration of:
- distributing the WordPress plugin without its source codes
- build and maintain plugins separately as separate NuGet packages
- maintaining/providing plugins with a dependency on a certain WordPress version
- signing the plugin with a private key
- possibly obfuscating the DLL
