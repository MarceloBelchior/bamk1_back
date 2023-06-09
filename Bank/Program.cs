﻿
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.App.API;

public class Program
{


    public static void Main(string[] args)
    {

        CreateHostBuilder(args)
                        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                       .Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           });
}



