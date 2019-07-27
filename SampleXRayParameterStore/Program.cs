using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SampleXRayParameterStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddSystemsManager(configureSource =>
                    {
                        configureSource.Path =
                            $"/Path_To_Paramter_Store";
                        configureSource.ReloadAfter = TimeSpan.FromMinutes(2);
                        configureSource.OnLoadException += exceptionContext =>
                        {
                            Console.WriteLine(
                                $"Error while Reading from System Parameter store {exceptionContext.Exception.StackTrace}");
                            exceptionContext.Ignore = true;
                        };
                        Console.WriteLine($"Configuration Parameters Read from System Parameter store");
                    });

                });
    }
}
