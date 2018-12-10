using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarService.Domain.Database
{
    public class CarServiceDbContext: DbContext
    {
        public CarServiceDbContext(DbContextOptions<CarServiceDbContext> options)
            : base(options)
        {

        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleMake>()
                .HasKey(v => v.VehicleMakeId);

            modelBuilder.Entity<VehicleModel>()
                .HasKey(v => v.VehicleModelId);

            modelBuilder.Entity<VehicleModel>()
                .HasOne(v => v.VehicleMake)
                .WithMany(v => v.VehicleModels)
                .HasForeignKey(v => v.VehicleMakeForeignKey);

            modelBuilder.Entity<Service>()
                .HasKey(s => s.ServiceId);
        }
    }

    public class VehicleMake
    {
        public int VehicleMakeId { get; set; }
        public string Name { get; set; }

        public List<VehicleModel> VehicleModels { get; set; }
    }
    public class VehicleModel
    {
        public int VehicleModelId { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }

        public int VehicleMakeForeignKey { get; set; }
        public VehicleMake VehicleMake { get; set; } 
    }
    
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
    }
}
