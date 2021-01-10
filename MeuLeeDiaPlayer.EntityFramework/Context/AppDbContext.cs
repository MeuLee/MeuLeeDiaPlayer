using MeuLeeDiaPlayer.EntityFramework.DbModels;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MeuLeeDiaPlayer.EntityFramework.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreatedAsync().GetAwaiter().GetResult();
        }

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
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<PlaylistSong>(entity =>
            {
                entity.HasKey(ps => new { ps.PlaylistId, ps.SongId });

                entity.HasOne(ps => ps.Playlist)
                    .WithMany(p => p.PlaylistSongs)
                    .HasForeignKey(ps => ps.PlaylistId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ps => ps.Song)
                    .WithMany(s => s.PlaylistSongs)
                    .HasForeignKey(ps => ps.SongId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.HasIndex(s => s.Path)
                    .IsUnique();
                entity.Property(s => s.Path)
                    .IsRequired();
            });
        }
    }
}
