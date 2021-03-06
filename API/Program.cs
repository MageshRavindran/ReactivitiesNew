using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();
           
            using (var scope = host.Services.CreateScope()){
                var service = scope.ServiceProvider;
                 try{
                var context = service.GetRequiredService<DataContext>();
                context.Database.Migrate();
            }
            catch (Exception ex){
                var logger = service.GetRequiredService<Logger<Program>>();
                logger.LogError(ex, "Exception occurred in Program level");
            }
            host.Run(); 
         }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
