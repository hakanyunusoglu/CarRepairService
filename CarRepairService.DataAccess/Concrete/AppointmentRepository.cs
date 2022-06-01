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
    public class AppointmentRepository : IAppointmentRepository
    {
        public Appointment Create(Appointment data)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                carDbContext.Appointments.Add(data);
                carDbContext.SaveChanges();
                return data;
            }
        }

        public void Delete(int id)
        {
            using (CarRepairServiceDataContext carDbContext = new CarRepairServiceDataContext())
            {
                var appointment = GetByID(id);
                if (appointment != null)
                {
                    appointment.isActive = false;
                    carDbContext.Appointments.Update(appointment);
                }
                carDbContext.SaveChanges();
            }
        }

        public List<Appointment> GetActiveAppointmentList(int id)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Appointments.Include(x => x.Car).ThenInclude(x => x.FuelType).Include(x => x.Car).ThenInclude(x => x.TransmissionType).Include(x => x.Car).ThenInclude(x => x.User).Include(x => x.User).Where(x=>x.isActive == true && x.userID == id).ToList();
            }
        }

        public List<Appointment> GetAllAppointmentList()
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Appointments.Include(x => x.Car).ThenInclude(x => x.FuelType).Include(x => x.Car).ThenInclude(x => x.TransmissionType).Include(x => x.Car).ThenInclude(x => x.User).Include(x => x.User).ToList();
            }
        }

        public Appointment GetByID(int id)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Appointments.Include(x => x.Car).ThenInclude(x=>x.FuelType).Include(x=>x.Car).ThenInclude(x=>x.TransmissionType).Include(x=>x.Car).ThenInclude(x=>x.User).Include(x => x.User).FirstOrDefault(x => x.ID == id);
            }
        }

        public List<Appointment> GetUserAppointments(int id)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Appointments.Where(x => x.userID == id).Include(x => x.Car).ThenInclude(x => x.FuelType).Include(x => x.Car).ThenInclude(x => x.TransmissionType).Include(x => x.Car).ThenInclude(x => x.User).ToList();
            }
        }

        public Appointment Update(Appointment data)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                carDbContext.Appointments.Update(data);
                carDbContext.SaveChanges();
                return data;
            }
        }
    }
}
