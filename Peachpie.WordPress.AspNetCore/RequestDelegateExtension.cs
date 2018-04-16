using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Pchp.Core;
using Peachpie.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peachpie.WordPress.AspNetCore
{
    public static class RequestDelegateExtension
    {
        static void ShortUrlRule(RewriteContext context)
        {
            var req = context.HttpContext.Request;
            var subpath = req.Path.Value;
            if (subpath != "/")
            {
                if (context.StaticFileProvider.GetFileInfo(subpath).Exists ||
                    subpath == "/favicon.ico") // 404
                {
                    // proceed to Static Files
                    return;
                }

                if (context.StaticFileProvider.GetDirectoryContents(subpath).Exists)
                {
                    var lastchar = subpath[subpath.Length - 1];
                    if (lastchar != '/' && lastchar != '\\')
                    {
                        // redirect to the directory with leading slash:
                        context.HttpContext.Response.Redirect(req.PathBase + subpath + "/" + req.QueryString, true);
                        context.Result = RuleResult.EndResponse;
                    }

                    // proceed to default document
                    return;
                }
            }

            // everything else is handled by `index.php`
            req.Path = new PathString("/index.php");
            context.Result = RuleResult.SkipRemainingRules;
        }

        static bool AllowCacheWpResponse(HttpContext context)
            => !context.Request.Cookies.Any(cookie => cookie.Key.StartsWith("wordpress_logged_in"));

        /// <summary>
        /// Installs WordPress middleware.
        /// </summary>
        public static IApplicationBuilder UseWordPress(this IApplicationBuilder app, WordPressConfig config)
        {
            // TODO: caching

            // url rewriting:
            app.UseRewriter(new RewriteOptions().Add(ShortUrlRule));

            // handling php files:
            app.UsePhp(new PhpRequestOptions()
            {
                ScriptAssembliesName = new[] { typeof(WpApp).Assembly.FullName },
                BeforeRequest = config.Apply,
            });

            // static files
            app.UseStaticFiles();

            return app;
        }
    }
}
