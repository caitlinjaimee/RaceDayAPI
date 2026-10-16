
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Models;

namespace RaceDayAPI.Data
{
    public class RaceDayContext : DbContext
    {
        public RaceDayContext(
            DbContextOptions<RaceDayContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Event> Events { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Enrollment> Enrollments { get; set; } = null!;

        public DbSet<Result> Results { get; set; } = null!;

        public DbSet<RaceDayAPI.Models.Route> Routes { get; set; } = null!;

        public DbSet<Weather> Weather { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RaceDayAPI.Models.Route>(entity =>
            {
                entity.ToTable("Routes");

                entity.HasKey(r => r.RouteID);

                entity.HasOne(r => r.Event)
                    .WithOne(e => e.Route)
                    .HasForeignKey<RaceDayAPI.Models.Route>(r => r.EventID);
            });

            modelBuilder.Entity<Weather>(entity =>
            {
                entity.ToTable("Weather");

                entity.HasKey(w => w.WeatherID);

                entity.Property(w => w.WeatherID)
                    .HasColumnName("WeatherID");

                entity.Property(w => w.EventID)
                    .HasColumnName("EventID");

                entity.Property(w => w.Temperature)
                    .HasColumnName("Temperature");

                entity.Property(w => w.WeatherCondition)
                    .HasColumnName("WeatherCondition");

                entity.Property(w => w.WindSpeed)
                    .HasColumnName("WindSpeed");

                entity.Property(w => w.ForecastDate)
                    .HasColumnName("ForecastDate");

                entity.HasOne(w => w.Event)
                    .WithOne(e => e.Weather)
                    .HasForeignKey<Weather>(w => w.EventID);
            });
        }
    }
}


