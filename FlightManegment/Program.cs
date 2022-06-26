using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManegment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseKestrel();
                    // webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseUrls("http://localhost:9000");
                    webBuilder.UseStartup<Startup>();

                }).ConfigureAppConfiguration((hostingContext, config) =>
                    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                   .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
                );
    }
}
