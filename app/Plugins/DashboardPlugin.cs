using Microsoft.AspNetCore.Mvc.ViewEngines;
using Pchp.Core;
using Peachpie.AspNetCore.Mvc;
using Peachpie.AspNetCore.Web;
using PeachPied.WordPress.Sdk;

namespace PeachPied.Demo.Plugins
{
    public class DashboardPlugin : IWpPlugin
    {
        public string Title { get; } = "Dashboard Widget";

        public DashboardPlugin()
        {
        }

        public void Configure(WpApp app)
        {
            app.DashboardWidget("peachpied.widget.1", "Razor Partial View", writer =>
            {
                app.Context.GetHttpContext().RenderPartialAsync(writer, "DashboardWidget", this);
            });
        }
    }
}
