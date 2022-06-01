using CarRepairService.DataAccess.Abstract;
using CarRepairService.DataAccess.DataContext;
using CarRepairService.Entities.DBO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.DataAccess.Concrete
{
    public class CarRepository : ICarRepository
    {
        public Car Add(Car data)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                carDbContext.Cars.Add(data);
                carDbContext.SaveChanges();
                return data;
            }
        }

        public void Delete(int id)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                var car = GetByID(id);
                if (car != null)
                {
                    car.isDeleted = true;
                    carDbContext.Cars.Update(car);
                }
                carDbContext.SaveChanges();
            }
        }

        public List<Car> GetAllList()
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Cars.Include(x => x.FuelType).Include(x => x.TransmissionType).Include(x => x.User).ToList();
            }
        }

        public Car GetByID(int id)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Cars.Include(x=>x.FuelType).Include(x=>x.TransmissionType).Include(x=>x.User).FirstOrDefault(x => x.ID == id);
            }
        }

        public List<FuelType> GetFuelTypeList()
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.FuelTypes.ToList();
            }
        }

        public List<Car> GetList()
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Cars.Where(x => x.isDeleted == false).Include(x => x.FuelType).Include(x => x.TransmissionType).Include(x => x.User).ToList();
            }
        }

        public List<Car> GetNotAppointmentList()
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                throw new NotImplementedException();
            }
        }

        public List<TransmissionType> GetTransmissionTypeList()
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.TransmissionTypes.ToList();
            }
        }

        public Car Update(Car data)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                carDbContext.Cars.Update(data);
                carDbContext.SaveChanges();
                return data;
            }
        }

        public List<Car> UserCarList(int id)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Cars.Where(x => x.isDeleted == false).Include(x => x.FuelType).Include(x => x.TransmissionType).Include(x => x.User).Where(x=>x.userID == id).ToList();
            }
        }
    }
}
