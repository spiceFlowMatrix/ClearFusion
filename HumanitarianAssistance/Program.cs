using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.DbEntities;
using HumanitarianAssistance.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HumanitarianAssistance
{
    public class Program
    {
    public static void Main(string[] args)
    {
      //BuildWebHost(args).Run();
      var host = BuildWebHost(args);

      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        try
        {
          var context = services.GetRequiredService<ApplicationDbContext>();
          var userManager = services.GetRequiredService<UserManager<AppUser>>();
          var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

          var dbInitializerLogger = services.GetRequiredService<ILogger<DbInitializer>>();
          DbInitializer.Initialize(context, userManager, roleManager, dbInitializerLogger).Wait();
        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "An error occurred while seeding the database.");
        }
      }
      host.Run();
    }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //.UseUrls("http://*:5000")
                //.UseUrls("http://*:5001")
                .UseUrls("http://*:5002")
                .Build();
    }
}
