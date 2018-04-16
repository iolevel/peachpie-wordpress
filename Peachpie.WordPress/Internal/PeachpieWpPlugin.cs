using System;
using System.Collections.Generic;
using System.Text;

namespace Peachpie.WordPress.Internal
{
    sealed class PeachpieWpPlugin : IWpPlugin
    {
        public static readonly PeachpieWpPlugin Instance = new PeachpieWpPlugin();

        private PeachpieWpPlugin() { }

        static string GetDashboardRightNow()
        {
            return string.Format(
                "<ul>" +
                "<li><img src='{0}' style='width:76px;margin:0 auto;display:block;'/></li>" +
                "<li>{1}</li>" +
                "</ul>",
                "https://raw.githubusercontent.com/peachpiecompiler/peachpie/master/docs/logos/round-orange-196x196.png",
                "<b>Hello from .NET!</b><br/>The site is powered by .NET Core instead of PHP, compiled entirely to MSIL bytecode using PeachPie.");
        }

        public void Configure(IWpApp app)
        {
            app.DashboardRightNow(GetDashboardRightNow);
        }
    }
}
