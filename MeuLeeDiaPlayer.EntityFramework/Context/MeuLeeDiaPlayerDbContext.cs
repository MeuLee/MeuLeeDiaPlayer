using MeuLeeDiaPlayer.EntityFramework.DbModels;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MeuLeeDiaPlayer.EntityFramework.Context
{
    public class MeuLeeDiaPlayerDbContext : DbContext
    {

        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Directory.CreateDirectory(Constants.DbLocation);
            optionsBuilder.UseSqlite($@"Data Source = {Constants.DbLocation}\{Constants.DbName};")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Songs)
                .WithMany(s => s.Playlists);

            modelBuilder.Entity<Playlist>()
                .Property(p => p.PlaylistName)
                .IsRequired();

            modelBuilder.Entity<Song>()
                .Property(s => s.Path)
                .IsRequired();

            modelBuilder.Entity<Song>()
                .HasIndex(s => s.Path)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
