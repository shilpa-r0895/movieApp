using Microsoft.EntityFrameworkCore;
using MovieApp.Entity;
using System;

namespace MovieApp.Repository
{
    public class MovieAppDbContext : DbContext
    {
        public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            optionsBuilder.UseSqlite(@"Data Source="+ filePath + "\\MovieApp.sdf");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovieMap>()
                .HasKey(am => new { am.MovieId, am.ActorId });
        }

        public DbSet<Person> People { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<ActorMovieMap> ActorMovieMap { get; set; }
    }
}
