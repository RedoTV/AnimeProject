using Microsoft.EntityFrameworkCore;

namespace Animes.Models
{
    public class AnimeContext : DbContext
    {
        public DbSet<Anime> Animes { get; set; } = null!;
        public AnimeContext(DbContextOptions<AnimeContext> options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
