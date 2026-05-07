using Cinema_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Cinema_Management_System.ViewModel;

namespace Cinema_Management_System.DataAccess
{
    public class ApplicationDbContext:IdentityDbContext <ApplicationUser> 
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }
        public ApplicationDbContext()
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>(
                e =>
                {
                    e.Property(e => e.Name).HasMaxLength(255);
                    e.Property(e => e.Price).HasPrecision(13, 3);
                    //e.Property(e => e.).HasMaxLength(255);/
                }
                );  
            modelBuilder.Entity<Actor>(
                e =>
                {
                    e.HasKey(e => e.ID);
                    e.Property(p => p.Name).HasMaxLength(255);
                    e.Property(p => p.Nationality).HasMaxLength(255);
                }
                );
            modelBuilder.Entity<Cinema>(
               e =>
               {
                   e.HasKey(e => e.ID);
                   e.Property(p => p.CinemaName).HasMaxLength(255);
                   e.Property(p => p.Location).HasMaxLength(255);
               }
               );
            modelBuilder.Entity<Category>(
               e =>
               {
                   e.HasKey(e => e.ID);
                   e.Property(p => p.HomeName).HasMaxLength(255);
                   //e.Property(p => p.Description).HasMaxLength(255);
               }
               );



        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MoviesSubimage> MoviesSubimages { get; set; }
        public DbSet<Cinema_Management_System.ViewModel.RegisterVM> RegisterVM { get; set; } = default!;
        public DbSet<Cinema_Management_System.ViewModel.LoginVM> LoginVM { get; set; } = default!;
    }
}
