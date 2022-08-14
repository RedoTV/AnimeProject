using Microsoft.EntityFrameworkCore;

namespace Animes.Models
{
    public class AnimeContext : DbContext
    {
        public DbSet<Anime> Animes { get; set; } = null!;
        public DbSet<AnimeSeries> Series { get; set; } = null!;
        public AnimeContext(DbContextOptions<AnimeContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anime>()
                .HasMany(c => c.Series)
                .WithOne(e => e.Anime);
        }
    }
}
