using CarRepairService.Entities.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.Business.Abstract
{
    public interface IAppointmentServices
    {
        List<Appointment> GetAllAppointmentList();
        List<Appointment> GetActiveAppointmentList(int id);
        Appointment GetByID(int id);
        Appointment Create(Appointment data);
        Appointment Update(Appointment data);
        void Delete(int id);
        List<Appointment> GetUserAppointments(int id);
    }
}
