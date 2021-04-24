using System;
using System.Collections.Generic;
using System.Composition;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Pchp.Core;
using PeachPied.WordPress.Standard;

namespace PeachPied.Demo.Plugins
{
    [Export(typeof(IWpPluginProvider))]
    class Provider : IWpPluginProvider
    {
        public IEnumerable<IWpPlugin> GetPlugins(IServiceProvider provider, string rootpath)
        {
            // provide plugins that will be loaded into wpdotnet:

            yield return new DashboardPlugin();
            yield return new ShortcodePlugin();
        }
    }
}
