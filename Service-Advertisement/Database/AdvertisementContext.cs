using Microsoft.EntityFrameworkCore;
using Service_Advertisement.Database.Interfaces;
using Service_Advertisement.Models;

namespace Service_Advertisement.Database
{
    public class AdvertisementContext : DbContext, IAdvertisementContext
    {
        protected readonly IConfiguration configuration;
        public AdvertisementContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Global.DatabaseConnectionString);
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
