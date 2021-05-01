using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Covid19Observer.API
{
    public class COVIDCasesContext: DbContext
    {
        public DbSet<Observation> covid_observations { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=COVIDCases;User ID = postgres; Password=postgres");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DatabaseInitializer.Initialize(modelBuilder);
        }
    }

    public class Observation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime ObservationDate { get; set; }
        public string Country { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
    }
}
