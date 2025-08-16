using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace LabourChowk_webapi.Data
{
    public class LabourChowkContextFactory: IDesignTimeDbContextFactory<LabourChowkContext>
    {
        public LabourChowkContext CreateDbContext(string[] args)
        {
            // Build config so EF Core CLI knows the connection string
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
             var optionsBuilder = new DbContextOptionsBuilder<LabourChowkContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlite(connectionString);

            return new LabourChowkContext(optionsBuilder.Options);
        }
    }
}