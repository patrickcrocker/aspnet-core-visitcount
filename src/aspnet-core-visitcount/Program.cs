using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace aspnet_core_visitcount
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    var host = new WebHostBuilder()
        //        .UseKestrel()
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .UseIISIntegration()
        //        .UseStartup<Startup>()
        //        .Build();

        //    host.Run();
        //}

        public static void Main(string[] args)
        {
            Console.WriteLine("[Main] main in");
            foreach (var item in (Environment.GetCommandLineArgs()))
            {
                Console.WriteLine("[Main.cmd] {0}", item);
            }

            foreach (string envKey in Environment.GetEnvironmentVariables().Keys)
            {
                Console.WriteLine("[Main.ENV] {0} [{1}]", envKey, Environment.GetEnvironmentVariable(envKey) ?? "");
            }

            var config = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}
