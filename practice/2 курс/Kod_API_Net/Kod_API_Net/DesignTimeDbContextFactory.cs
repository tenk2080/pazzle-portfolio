using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using practice.Data;

namespace practice.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=kod_api_net.db");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}