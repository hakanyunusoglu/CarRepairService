using CarRepairService.Entities.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.DataAccess.Abstract
{
    public interface ICarRepository
    {
        List<Car> GetList();
        List<Car> GetAllList();
        List<Car> GetNotAppointmentList();
        Car GetByID(int id);
        Car Add(Car data);
        Car Update(Car data);
        void Delete(int id);
        List<FuelType> GetFuelTypeList();
        List<TransmissionType> GetTransmissionTypeList();
        List<Car> UserCarList(int id);
    }
}
