using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Peachpie.WordPress.AspNetCore;
using System;
using System.IO;
using System.Linq;

namespace peachserver
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://*:5004/")
                .Build();

            host.Run();
        }
    }

    class Startup
    {
        static void UseWpResponseCaching(IApplicationBuilder app)
        {
            // first hack the request headers:
            app.Use(async (context, next) =>
            {
                if (AllowCacheWpResponse(context))
                {
                    // public by default
                    context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromMinutes(5),
                    };

                    context.Request.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromMinutes(5),
                    };
                }

                // perform the request, WP will set CacheControl to no-cache if necessary:
                await next();
            });

            // proceed with caching mechanism: (it checks Cache-Control headers in both Request and Response)
            app.UseResponseCaching();
        }

        static bool AllowCacheWpResponse(HttpContext context)
            => !context.Request.Cookies.Any(cookie => cookie.Key.StartsWith("wordpress_logged_in"));

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            //UseWpResponseCaching(app);
            app.UseWordPress(new WordPressConfig()
            {
                DbHost = "localhost",
                DbPassword = "password",
                DbUser = "root",
                DbName = "wordpress",
            });

            app.UseDefaultFiles();
        }
    }
}
