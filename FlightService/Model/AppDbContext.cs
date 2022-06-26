using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .Property(u => u.PNR)
                .HasComputedColumnSql("N'PNR' + RIGHT('00000'+CAST(PNRNo as NVARCHAR(5)),5)");
        }
        public DbSet<Airline> Airline { get; set; }
        public DbSet<ScheduleAirline> ScheduleAirline { get; set; }
        public DbSet<Booking> Booking { get; set; }


    }
}
