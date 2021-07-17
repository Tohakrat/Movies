using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MvcMovieContext-1;Trusted_Connection=True;MultipleActiveResultSets=true");
        }                              
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Movie>().HasData(
                new Movie[]
                {
                new Movie {Id = 0, Title="Rembo"},
                new Movie {Id = 1, Title="Джентльмены удачи"},
                new Movie {Id = 2, Title="Криминальное чтиво"}
                });   */         
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        //public ApplicationContext()
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
