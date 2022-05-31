using Microsoft.EntityFrameworkCore;
using Service_Advertisement.Models;

namespace Service_Advertisement.Database.Interfaces
{
    public interface IAdvertisementContext : IDbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
