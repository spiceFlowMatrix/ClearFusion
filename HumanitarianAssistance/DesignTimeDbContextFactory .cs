using HumanitarianAssistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance
{
  public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
  {
    public ApplicationDbContext CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();
      var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
      var connectionString = configuration.GetConnectionString("linuxdb");
      builder.UseNpgsql(connectionString);
      return new ApplicationDbContext(builder.Options);
    }
  }
}
