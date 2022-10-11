using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Data
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) { }

        public DbSet<City> Citys { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("São Paulo")
                {
                    Id = 1,
                    Description = "Cidade localizada no Brazil"
                },
                new City("Belo Horizonte")
                {
                    Id = 2,
                    Description = "Cidade localizada no Brazil"
                },
                new City("Porto Alegre")
                {
                    Id = 3,
                    Description = "Cidade localizada no Brazil"
                });

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Neo Quimica Arena")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "Estadio de futebol"
                },
                new PointOfInterest("Mineirão")
                {
                    Id = 2,
                    CityId = 2,
                    Description = "Estadio de futebol"
                },
                new PointOfInterest("Beira Rio")
                {
                    Id = 3,
                    CityId = 3,
                    Description = "Estadio de futebol"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}