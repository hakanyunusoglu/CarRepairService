using CarRepairService.Entities.DBO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRepairService.Web.Models
{
    public class CarVM
    {
        public Car Car { get; set; }
        public List<Car> carList { get; set; }
        public List<FuelType> fuelTypeList { get; set; }
        public List<TransmissionType> transmissionTypeList { get; set; }
        public List<User> userList { get; set; }

    }
}
