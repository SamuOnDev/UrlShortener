using Microsoft.EntityFrameworkCore;
using UrlShortenerApiBackend.Models.DataModels;

namespace UrlShortenerApiBackend.DataAcces
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UrlList> UrlLists { get; set; }
    }
}
