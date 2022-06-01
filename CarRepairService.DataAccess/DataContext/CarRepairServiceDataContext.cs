using CarRepairService.Entities.DBO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.DataAccess.DataContext
{
    public class CarRepairServiceDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.; Database=CarRepairServiceDB; Trusted_Connection=true;");
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API SIDE

            //KEYS
           // modelBuilder.Entity<Car>().HasKey(x => x.ID);
           // modelBuilder.Entity<FuelType>().HasKey(x => x.ID);
           // modelBuilder.Entity<TransmissionType>().HasKey(x => x.ID);
           // modelBuilder.Entity<User>().HasKey(x => x.ID);

            //CAR CLASS ANNOTATIONS
           // modelBuilder.Entity<Car>().Property(x => x.carBrand).IsRequired();
           // modelBuilder.Entity<Car>().Property(x => x.lastMaintenanceDate).IsRequired();
           // modelBuilder.Entity<Car>().Property(x => x.modelYear).IsRequired();

            //CAR AND TRANSMISSIONTYPE RELATIONSHIP WITH MANY TO ONE
          // modelBuilder.Entity<TransmissionType>().HasMany(x => x.Cars).WithOne(x => x.TransmissionType).HasForeignKey(x => x.transmissionTypeID).OnDelete(DeleteBehavior.Cascade);

            //CAR AND FUELTYPE RELATIONSHIP WİTH MANY TO ONE
           // modelBuilder.Entity<FuelType>().HasMany(x => x.Cars).WithOne(x => x.FuelType).HasForeignKey(x => x.fuelTypeID).OnDelete(DeleteBehavior.Cascade);


            //CAR AND OWNER RELATIONSHOP WİTH ONE TO ONE
            //modelBuilder.Entity<Car>().HasOne(x => x.User).WithOne(x => x.Car).HasForeignKey<Car>(x => x.ownerID);

            //SEED DATA
            this.SeedUser(modelBuilder);
            this.SeedFuelType(modelBuilder);
            this.SeedTransmissionType(modelBuilder);


            base.OnModelCreating(modelBuilder);
        }
        private void SeedUser(ModelBuilder builder)
        {
            byte[] passwordHash, passwordSalt;
            string password = "12345";
            var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            User user = new User()
            {
                ID = 1,
                firstName = "Hakan",
                lastName = "Yunusoğlu",
                Description = "Yazılım Mühendisi",
                Username = "hakan",
                Role = 3,
                isActive = true,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt
            };
            builder.Entity<User>().HasData(user);
        }
        private void SeedFuelType(ModelBuilder builder)
        {
            List<FuelType> fuelTypeList = new List<FuelType>()
            {
                new FuelType()
                {
                    ID = 1,
                    Type = "Gasoline"
                },
                new FuelType()
                {
                    ID = 2,
                    Type = "Diesel"
                },
                new FuelType()
                {
                    ID=3,
                    Type = "LPG"
                },
                new FuelType()
                {
                    ID = 4,
                    Type = "Gasoline/LPG"
                },
                new FuelType()
                {
                    ID = 5,
                    Type = "Hybrid"
                }
            };
            builder.Entity<FuelType>().HasData(fuelTypeList);
        }
        private void SeedTransmissionType(ModelBuilder builder)
        {
            List<TransmissionType> transmissionTypeList = new List<TransmissionType>()
            {
                new TransmissionType()
                {
                    ID = 1,
                    Type = "Manual"
                },
                new TransmissionType()
                {
                    ID = 2,
                    Type = "Automatic"
                },
                new TransmissionType()
                {
                    ID = 3,
                    Type = "Semi Automatic"
                }
            };
            builder.Entity<TransmissionType>().HasData(transmissionTypeList);
        } 
    }
}
