This project demonstrates how to start an ASP.NET Core server, integrate WordPress into its pipeline and add additional PHP plugins into it.

# WordPress

Even though the project is noticeably small, it contains the entire WordPress CMS. How? This complex PHP web framework was already compiled into the .NET Standard library (see [PeachPie compiler platform](https://github.com/peachpiecompiler/peachpie) for more information) and packed into a NuGet package. This demo may just reference it.

To find out more about PeachPie, check out our [resources](https://www.peachpie.io/resources), which includes tutorials, benchmarks and articles on PeachPie.

# WordPress PHP Plugin

With this approach it is finally possible to have a project containing only the plugin. WordPress itself is already compiled and the plugin has a dependency on it (like any other C# app you would make).

The build process will do following:
- compiles `.php` files within the `PeachPied.WordPress.MyPlugin` directory. Note that the path `wp-content/plugins` matters.
- packs the compiled DLL together with content files (like images, scripts etc.) into a NuGet package.
- copies the project content into the application output directory, together with WordPress content files extracted from its NuGet.

# WordPress C# plugin

The project takes advantage of the `wpdotnet SDK` which provides a C# abstraction `IWpPlugin`. Objects of this type can be passed to the WordPress middleware (see `UseWordPress`) and hook onto filters and actions available in the WordPress API.

// TODO: add link to additional info about the wpdotnet SDK & WordPress API

# Configuration

The configuration itself serves as a demonstration of combining a legacy PHP application with .NET. The demo takes advantage of the ASP.NET Core configuration mechanism, namely the files `appsettings.json`, `appsettings.deployment.json` and `appsettings.production.json`. This allows for having a different configuration for each environment without the need of altering the source code of the project.

// TODO: add read more about asp.net core config

# Sourceless Distribution

The WordPress package was already configured, so the original PHP source code is not a part of the NuGet nor of the compiled DLL. In case of additional plugins (see `PeachPied.WordPress.MyPlugin`), it depends what source files are needed in run time. Usually only the main plugin file (containing the WordPress metadata) has to be distributed together with the DLL, so that WordPress can read its content and work properly. The other files can be excluded from the package.

As a result, this project serves as a demonstration of:
- distributing the WordPress plugin without its source codes
- build and maintain plugins separately as separate NuGet packages
- maintaining/providing plugins with a dependency on a certain WordPress version
- signing the plugin with a private key
- possibly obfuscating the DLL
