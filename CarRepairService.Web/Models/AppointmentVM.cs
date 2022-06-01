using CarRepairService.Entities.DBO;
using System.Collections.Generic;

namespace CarRepairService.Web.Models
{
    public class AppointmentVM
    {
        public Appointment Appointment { get; set; }
        public List<Appointment> appointmentList { get; set; }
        public List<Car> carList { get; set; }
        public List<FuelType> fuelTypeList { get; set; }
        public List<TransmissionType> transmissionTypeList { get; set; }
        public List<User> userList { get; set; }
    }
}
