using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Sample.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .UseSerilog((hostingContext, loggerConfiguration) =>
                 {
                     loggerConfiguration
                         .ReadFrom.Configuration(hostingContext.Configuration)
                         .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                         .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);
                 })
                   .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }
}
