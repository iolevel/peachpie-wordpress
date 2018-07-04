This project demonstrates how to start ASP.NET Core server, integrate WordPress into its pipeline and add additional PHP plugins into it.

# WordPress

Even the project is noticeably small, it contains the entire WordPress CMS. How? This complex PHP web framework was already compiled into .NET Standard library (see PeachPie compiler platform for more information) and packed into a NuGet package. This demo may just reference it.

// TODO: read more about PeachPie

# WordPress PHP Plugin

With this approach it is finally possible to have a project only containing the plugin. The WordPress itself is already compiled and the plugin has a dependency on it (like any other C# app you would make).

The build process will do following:
- compiles `.php` files within `PeachPied.WordPress.MyPlugin` directory. Note the path `wp-content/plugins` matters.
- packs the compiled DLL together with content files (like images, scripts etc.) into a NuGet package
- copies the project content into the application output directory, together with WordPress content files extracted from its NuGet.

# WordPress C# plugin

The project takes advantage of `wpdotnet SDK` which provides a C# abstraction `IWpPlugin`. Objects of this type can be passed to WordPress middleware (see `UseWordPress`) and hook into filters and actions available in WordPress API.

// TODO: read more about wpdotnet SDK, WordPress API

# Configuration

The configuration itself serves as a demonstration of combining legacy PHP application with .NET. The demo takes advantage of ASP.NET Core configuration mechanism, namely files `appsettings.json`, `appsettings.deployment.json` and `appsettings.production.json`. This allows for having a different configuration for each environment without the need of altering the source code of the project.

// TODO: read more about asp.net core config

# Sourceless Distribution

The WordPress package was already configured so the original PHP source code is not a part of the NuGet nor compiled DLL. In case of additional plugins (see `PeachPied.WordPress.MyPlugin`) it depends what source files are needed in run time. Usually only the main plugin file (containing the WordPress metadata) has to be distributed together with the DLL so WordPress can read its content and work properly. The other files can be excluded from the package.

In result, this project serves as a demonstration of:
- distributing the WordPress plugin without its source codes
- build and maintain plugins separately as separate NuGet packages
- maintaining/providing plugins with dependency on certain WordPress version
- signing the plugin with a private key
- possibly obfuscating the DLL
