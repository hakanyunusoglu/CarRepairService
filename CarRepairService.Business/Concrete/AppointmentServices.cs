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
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IAppointmentRepository repo;
        public AppointmentServices(IAppointmentRepository _repo)
        {
            repo = _repo;
        }
        public Appointment Create(Appointment data)
        {
            return repo.Create(data);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public List<Appointment> GetActiveAppointmentList(int id)
        {
            return repo.GetActiveAppointmentList(id);
        }

        public List<Appointment> GetAllAppointmentList()
        {
            return repo.GetAllAppointmentList();
        }

        public Appointment GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public List<Appointment> GetUserAppointments(int id)
        {
           return repo.GetUserAppointments(id);
        }

        public Appointment Update(Appointment data)
        {
            return repo.Update(data);
        }
    }
}
