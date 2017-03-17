using Microsoft.AspNetCore.Builder;
using Peachpie.Web;

namespace peachserver
{
    class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            Pchp.Core.Context.DefaultErrorHandler = new Pchp.Core.CustomErrorHandler(); // disable debug asserts

            app.UsePhp(new PhpRequestOptions() { ScriptAssembliesName = new[] { "website" } });
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
