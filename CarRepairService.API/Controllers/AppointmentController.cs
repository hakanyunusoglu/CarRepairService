using CarRepairService.API.DTO;
using CarRepairService.Business.Abstract;
using CarRepairService.Entities.DBO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRepairService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private IAppointmentServices repo;
        public AppointmentController(IAppointmentServices _repo)
        {
            repo = _repo;
        }
        [HttpGet("[action]/{id}")]
        public IActionResult GetActiveAppointmentList(int id)
        {
            var appoList = repo.GetActiveAppointmentList(id);
            if (appoList != null)
            {
                return Ok(appoList);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("[action]")]
        public IActionResult GetAllAppointmentList()
        {
            var appoList = repo.GetAllAppointmentList();
            if (appoList != null)
            {
                return Ok(appoList);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("{id}")]
        public IActionResult GetAppointmentByID(int id)
        {
            var appo = repo.GetByID(id);
            if (appo != null)
            {
                return Ok(appo);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("[action]/{id}")]
        public IActionResult GetUserAppointments(int id)
        {
            var appoList = repo.GetUserAppointments(id);
            if (appoList != null)
            {
                return Ok(appoList);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("[action]")]
        public IActionResult AppointmentCreate(AppointmentVM model)
        {
            if (ModelState.IsValid)
            {
                Appointment appo = new Appointment();
                try
                {
                    appo.ID = model.ID;
                    appo.isActive = model.isActive;
                    appo.StartDate = model.StartDate;
                    appo.EndDate = model.EndDate;
                    appo.Description = model.Description;
                    appo.carID = model.carID;
                    appo.userID = model.userID;
                    appo.Technician = model.Technician;
                    repo.Create(appo);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return NotFound("Randevu oluşturma işlemi sırasında bir hata oluştu! Hata :" + ex);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("[action]")]
        public IActionResult AppointmentUpdate(AppointmentVM model)
        {
            if (repo.GetByID(model.ID) != null)
            {
                Appointment appo = new Appointment();
                try
                {
                    appo.ID = model.ID;
                    appo.isActive = model.isActive;
                    appo.StartDate = model.StartDate;
                    appo.EndDate = model.EndDate;
                    appo.Description = model.Description;
                    appo.carID = model.carID;
                    appo.userID = model.userID;
                    appo.Technician = model.Technician;
                    repo.Update(appo);
                    return Ok(appo);
                }
                catch (Exception ex)
                {
                    return NotFound("Güncelleme işlemi sırasında hata oluştu! Hata :" + ex);
                }
            }
            else
            {
                return NotFound("Güncelleme yapılacak randevu bulunamadı!");
            }
        }
        [HttpGet("[action]/{id}")]
        public IActionResult AppointmentDelete(int id)
        {
            if (repo.GetByID(id) != null)
            {
                try
                {
                    repo.Delete(id);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return NotFound("Silme işlemi sırasında hata oluştu! Hata :" + ex);
                }
            }
            else
            {
                return NotFound("Silinecek randevu bulunamadı!");
            }
        }
    }
}
