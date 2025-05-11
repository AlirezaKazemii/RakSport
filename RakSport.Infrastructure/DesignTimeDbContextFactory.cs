using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RakSport.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
    {
        public DataBaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // تعیین مسیر فعلی
                .AddJsonFile("appsettings.json")  // فایل تنظیمات
                .Build();
            
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("RakSport"));

            return new DataBaseContext(optionsBuilder.Options);
        }
    }
}
