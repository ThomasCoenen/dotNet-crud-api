//create a new project
//dotnet new webapi -n Catelog

//CHECK Catelog.csproj -> missing 2 dependencies that he has

//appsettings.json  -> declare configurations 
//dotnet run   -> runs app
//https://localhost:5001

//trust the cert that comes w/ .net sdk
//dotnet dev-certs https --trust

//lsof -P | grep ':5001' | awk '{print $2}' | xargs kill -9

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Catelog
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
                    webBuilder.UseStartup<Startup>();
                });
    }
}
