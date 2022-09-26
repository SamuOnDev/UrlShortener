using Microsoft.EntityFrameworkCore;
using UrlShortenerApiBackend.Models.DataModels;

namespace UrlShortenerApiBackend.DataAcces
{
    public class UrlShortenerDBContext : DbContext
    {
        public UrlShortenerDBContext(DbContextOptions<UrlShortenerDBContext> options) : base(options)
        {

        }

        public DbSet<User>? Users { get; set; }
        public DbSet<UrlList> UrlLists { get; set; }
    }
}
