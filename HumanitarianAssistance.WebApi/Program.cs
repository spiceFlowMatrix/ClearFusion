using System;
using System.IO;
using DataAccess.Data;
using DataAccess.DbEntities;
using HumanitarianAssistance.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace HumanitarianAssistance.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                            .Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    services.GetRequiredService<ApplicationDbContext>();
                    services.GetRequiredService<UserManager<AppUser>>();
                    services.GetRequiredService<RoleManager<IdentityRole>>();
                    services.GetRequiredService<ILogger<DbInitializer>>();
                    // DbInitializer.Initialize(context, userManager, roleManager, dbInitializerLogger).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseUrls("http://*/newui");
        //.UseUrls("http://*:5004");
    }
}
