using HumanitarianAssistance.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Persistence
{
    public class HumanitarianAssistanceDbContextFactory : DesignTimeDbContextFactoryBase<HumanitarianAssistanceDbContext>
    {
        protected override HumanitarianAssistanceDbContext CreateNewInstance(DbContextOptions<HumanitarianAssistanceDbContext> options)
        {
            return new HumanitarianAssistanceDbContext(options);
        }
    }
}
