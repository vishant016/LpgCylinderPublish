using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Azure.Identity;

namespace LPGCylinderSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((context, config) =>
{
    if (context.HostingEnvironment.IsProduction())
    {
        var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
        config.AddAzureKeyVault(
        keyVaultEndpoint,
        new DefaultAzureCredential());
    }
})
             .ConfigureLogging((hostingContext, logging) =>
             {
                 logging.ClearProviders();
                 logging.AddConsole(options => options.IncludeScopes = true);
                 logging.AddDebug();
             })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
