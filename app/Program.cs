using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace peachserver
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseWebRoot(Path.GetDirectoryName(Directory.GetCurrentDirectory()) + "/wwwroot")
                .UseUrls("http://*:5004/")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
