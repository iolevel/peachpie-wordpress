using Microsoft.AspNetCore.Mvc.ViewEngines;
using Peachpie.AspNetCore.Mvc;
using Peachpie.AspNetCore.Web;
using PeachPied.WordPress.Sdk;

namespace PeachPied.Demo.Plugins
{
    public class DashboardPlugin : IWpPlugin
    {
        readonly IViewEngine _viewEngine;

        public string Title { get; } = "Dashboard Widget";

        public DashboardPlugin(ICompositeViewEngine viewEngine)
        {
            _viewEngine = viewEngine;
        }

        public void Configure(WpApp app)
        {
            app.DashboardWidget("peachpied.widget.1", "Razor Partial View", writer =>
            {
                // TODO: this is still a technical demo, it does not allow to use a bunch of Razor View features
                app.Context.GetHttpContext().RenderViewAsync(writer, _viewEngine, "DashboardWidget", this);
            });
        }
    }
}
