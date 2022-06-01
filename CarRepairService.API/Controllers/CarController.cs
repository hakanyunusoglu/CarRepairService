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
    public class CarController : ControllerBase
    {
        private ICarServices repo;
        public CarController(ICarServices _repo)
        {
            repo = _repo;
        }
        [HttpGet("[action]/{id}")]
        public IActionResult GetActiveCarList(int id)
        {
            var carList = repo.UserCarList(id);
            return Ok(carList);
        }
        [HttpGet("[action]")]
        public IActionResult GetAllCarList()
        {
            var carList = repo.GetAllList();
            if (carList != null)
            {
                return Ok(carList);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetCarByID(int id)
        {
            var car = repo.GetByID(id);
            if (car != null)
            {
                return Ok(car);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("[action]")]
        public IActionResult CarUpdate(CarVM model)
        {
            if (repo.GetByID(model.ID) != null)
            {
                Car car = new Car();
                try
                {
                    car.ID = model.ID;
                    car.modelYear = model.modelYear;
                    car.carBrand = model.carBrand;
                    car.lastMaintenanceDate = model.lastMaintenanceDate;
                    car.carImage = "car01.jpg";
                    car.isDeleted = model.isDeleted;
                    car.userID = model.userID;
                    car.fuelTypeID = model.fuelTypeID;
                    car.transmissionTypeID = model.transmissionTypeID;
                    repo.Update(car);
                }
                catch (Exception ex)
                {
                    return NotFound("Güncelleme işlemi sırasında hata oluştu! Hata :" + ex);
                }
                return Ok(car);
            }
            else
            {
                return NotFound("Güncelleme yapılacak araç bulunamadı!");
            }
        }
        [HttpGet("[action]/{id}")]
        public IActionResult CarDelete(int id)
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
                return NotFound("Silinecek araç bulunamadı!");
            }
        }
        [HttpPost("[action]")]
        public IActionResult CarCreate(CarVM model)
        {
            if (ModelState.IsValid)
            {
                Car car = new Car();
                try
                {
                    car.ID = model.ID;
                    car.modelYear = model.modelYear;
                    car.carBrand = model.carBrand;
                    car.lastMaintenanceDate = model.lastMaintenanceDate;
                    car.carImage = "car01.jpg";
                    car.isDeleted = model.isDeleted;
                    car.userID = model.userID;
                    car.fuelTypeID = model.fuelTypeID;
                    car.transmissionTypeID = model.transmissionTypeID;
                    repo.Add(car);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return NotFound("Araç ekleme işlemi sırasında bir hata oluştu! Hata :" + ex);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet("[action]/{id}")]
        public IActionResult GetUserCars(int id)
        {
            var car = repo.UserCarList(id);
            if (car != null)
            {
                return Ok(car);
            }
            return BadRequest(ModelState);
        }
    }
}
