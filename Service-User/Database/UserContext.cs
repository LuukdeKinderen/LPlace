using Microsoft.EntityFrameworkCore;
using Service_User.Models;

namespace Service_User.Database
{
    public class UserContext : DbContext
    {
        

        protected readonly IConfiguration configuration;
        public UserContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<User> Users { get; set; }

    }
}
