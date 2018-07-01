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
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            
            app.UseWordPress(new WordPressConfig()
            {
                // TODO: settings.json
                DbHost = "localhost",
                DbPassword = "password",
                DbUser = "root",
                DbName = "wordpress",
            });

            app.UseDefaultFiles();
        }
    }
}
