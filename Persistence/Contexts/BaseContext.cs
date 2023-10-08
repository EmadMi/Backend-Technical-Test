using Application.Interfaces.Contexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseContext : DbContext, IBaseContext
    {
        public BaseContext(DbContextOptions Options) : base(Options)
        {

        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<AreaRule> AreaRules { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            //
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle { Id = 1, Name = "Emergency", IsTaxFree = true, Order = 1 });
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle { Id = 2, Name = "Bus", IsTaxFree = true, Order = 2 });
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle { Id = 3, Name = "Diplomat", IsTaxFree = true, Order = 3 });
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle { Id = 4, Name = "Motorcycle", IsTaxFree = true, Order = 4 });
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle { Id = 5, Name = "Military", IsTaxFree = true, Order = 5 });
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle { Id = 6, Name = "Foreign", IsTaxFree = true, Order = 6 });
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle { Id = 7, Name = "Car", IsTaxFree = false, Order = 7 });
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle { Id = 8, Name = "Motorbike", IsTaxFree = false, Order = 8 });
            //
            modelBuilder.Entity<Area>().HasData(new Area { Id = 1, Name = "Gothenburg", MaxTaxFee = 60, Order = 1 });
            //
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 1, HolyDate = new DateTime(2013, 01, 01) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 2, HolyDate = new DateTime(2013, 03, 28) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 3, HolyDate = new DateTime(2013, 03, 29) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 4, HolyDate = new DateTime(2013, 04, 01) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 5, HolyDate = new DateTime(2013, 04, 30) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 6, HolyDate = new DateTime(2013, 05, 01) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 7, HolyDate = new DateTime(2013, 05, 08) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 8, HolyDate = new DateTime(2013, 05, 09) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 9, HolyDate = new DateTime(2013, 06, 05) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 10, HolyDate = new DateTime(2013, 06, 06) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 11, HolyDate = new DateTime(2013, 06, 21) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 12, HolyDate = new DateTime(2013, 11, 01) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 13, HolyDate = new DateTime(2013, 12, 24) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 14, HolyDate = new DateTime(2013, 12, 25) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 15, HolyDate = new DateTime(2013, 12, 26) });
            modelBuilder.Entity<Holiday>().HasData(new Holiday { Id = 16, HolyDate = new DateTime(2013, 12, 31) });
            //
            modelBuilder.Entity<AreaRule>().HasData(new AreaRule { Id = 1, AreaId = 1, StartTime = new TimeSpan(6, 00, 00), EndTime = new TimeSpan(6, 29, 00), TaxFee = 8, Year = 2013 });
            modelBuilder.Entity<AreaRule>().HasData(new AreaRule { Id = 2, AreaId = 1, StartTime = new TimeSpan(6, 30, 00), EndTime = new TimeSpan(6, 59, 00), TaxFee = 13, Year = 2013 });
            modelBuilder.Entity<AreaRule>().HasData(new AreaRule { Id = 3, AreaId = 1, StartTime = new TimeSpan(7, 00, 00), EndTime = new TimeSpan(7, 59, 00), TaxFee = 18, Year = 2013 });
            modelBuilder.Entity<AreaRule>().HasData(new AreaRule { Id = 4, AreaId = 1, StartTime = new TimeSpan(8, 00, 00), EndTime = new TimeSpan(8, 29, 00), TaxFee = 13, Year = 2013 });
            modelBuilder.Entity<AreaRule>().HasData(new AreaRule { Id = 5, AreaId = 1, StartTime = new TimeSpan(8, 30, 00), EndTime = new TimeSpan(14, 59, 00), TaxFee = 8, Year = 2013 });
            modelBuilder.Entity<AreaRule>().HasData(new AreaRule { Id = 6, AreaId = 1, StartTime = new TimeSpan(15, 00, 00), EndTime = new TimeSpan(15, 29, 00), TaxFee = 13, Year = 2013 });
            modelBuilder.Entity<AreaRule>().HasData(new AreaRule { Id = 7, AreaId = 1, StartTime = new TimeSpan(15, 30, 00), EndTime = new TimeSpan(16, 59, 00), TaxFee = 18, Year = 2013 });
            modelBuilder.Entity<AreaRule>().HasData(new AreaRule { Id = 8, AreaId = 1, StartTime = new TimeSpan(17, 00, 00), EndTime = new TimeSpan(17, 59, 00), TaxFee = 13, Year = 2013 });
            modelBuilder.Entity<AreaRule>().HasData(new AreaRule { Id = 9, AreaId = 1, StartTime = new TimeSpan(18, 00, 00), EndTime = new TimeSpan(18, 29, 00), TaxFee = 8, Year = 2013 });
            modelBuilder.Entity<AreaRule>().HasData(new AreaRule { Id = 10, AreaId = 1, StartTime = new TimeSpan(18, 30, 00), EndTime = new TimeSpan(5, 59, 00), TaxFee = 0, Year = 2013 });
            //base.OnModelCreating(modelBuilder);
        }
    }
}
