using CarRepairService.Business.Abstract;
using CarRepairService.DataAccess.Abstract;
using CarRepairService.Entities.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.Business.Concrete
{
    public class CarServices : ICarServices
    {
        private ICarRepository repo;
        public CarServices(ICarRepository _repo)
        {
            repo = _repo;
        }

        public  Car Add(Car data)
        {
            return  repo.Add(data);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public List<Car> GetAllList()
        {
            return repo.GetAllList();
        }

        public Car GetByID(int id)
        {
            return  repo.GetByID(id);
        }

        public List<FuelType> GetFuelTypeList()
        {
            return repo.GetFuelTypeList();
        }

        public List<Car> GetList()
        {
            return repo.GetList();
        }

        public List<Car> GetNotAppointmentList()
        {
            return repo.GetNotAppointmentList();
        }

        public List<TransmissionType> GetTransmissionTypeList()
        {
            return repo.GetTransmissionTypeList();
        }

        public Car Update(Car data)
        {
            return repo.Update(data);
        }

        public List<Car> UserCarList(int id)
        {
            return repo.UserCarList(id);
        }
    }
}
