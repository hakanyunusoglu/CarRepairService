using CarRepairService.Business.Abstract;
using CarRepairService.Entities.DBO;
using CarRepairService.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CarRepairService.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentServices repo;
        private readonly IUserServices uRepo;
        private readonly ICarServices carRepo;
        public AppointmentController(IAppointmentServices _repo, IUserServices _uRepo, ICarServices _carRepo)
        {
            repo = _repo;
            uRepo = _uRepo;
            carRepo = _carRepo;
        }
        [HttpGet]
        public IActionResult AppointmentList()
        {
            if (HttpContext.Session.GetInt32("userRole") == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            else
            {
                string username = HttpContext.Session.GetString("loggedUserName").ToString();
                var user = uRepo.GetByUsername(username);
                if (user != null)
                {
                    AppointmentVM model = new AppointmentVM();
                    try
                    {
                        model.userList = uRepo.GetAllList();
                        model.carList = carRepo.GetAllList();
                        model.fuelTypeList = carRepo.GetFuelTypeList();
                        model.transmissionTypeList = carRepo.GetTransmissionTypeList();
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync($"Appointment/GetActiveAppointmentList/{user.ID}");
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var appo = readTask.Result;
                            model.appointmentList = JsonConvert.DeserializeObject<List<Appointment>>(appo);
                            return View(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        return NotFound("Randevuları listeleme işlemi sırasında hata oluştu! Hata: " + ex);
                    }
                }
            }
            ViewBag.CheckUser = "empty";
            return View();
        }
        [HttpGet]
        public IActionResult AllAppointmentList()
        {
            if (HttpContext.Session.GetInt32("userRole") == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            else
            {
                string username = HttpContext.Session.GetString("loggedUserName").ToString();
                var user = uRepo.GetByUsername(username);
                if (user != null)
                {
                    AppointmentVM model = new AppointmentVM();
                    try
                    {
                        model.userList = uRepo.GetAllList();
                        model.carList = carRepo.GetAllList();
                        model.fuelTypeList = carRepo.GetFuelTypeList();
                        model.transmissionTypeList = carRepo.GetTransmissionTypeList();
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync($"Appointment/GetUserAppointments/{user.ID}");
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var appo = readTask.Result;
                            model.appointmentList = JsonConvert.DeserializeObject<List<Appointment>>(appo);
                            return View(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        return NotFound("Randevuları listeleme işlemi sırasında hata oluştu! Hata: " + ex);
                    }
                }
            }
            ViewBag.CheckUser = "empty";
            return View();
        }
        public IActionResult CreateAppointment()
        {
            string username = null;
            if (HttpContext.Session.GetInt32("userRole") == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            else
            {
                try
                {
                    username = HttpContext.Session.GetString("loggedUserName").ToString();
                    var user = uRepo.GetByUsername(username);
                    if (user != null)
                    {
                        AppointmentVM appoVM = new AppointmentVM();
                        appoVM.carList = carRepo.UserCarList(user.ID);
                        appoVM.transmissionTypeList = carRepo.GetTransmissionTypeList();
                        appoVM.fuelTypeList = carRepo.GetFuelTypeList();
                        appoVM.userList = uRepo.GetTechnicianList();
                        return View(appoVM);
                    }
                    else
                    {
                        return RedirectToAction("IndexUserView", "Car");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Randevu oluşturma işlemi sırasında hata oluştu! Hata: " + ex);
                }
            }
        }
        [HttpPost]
        public IActionResult CreateAppointment(AppointmentVM data)
        {
            string username = null;

            username = HttpContext.Session.GetString("loggedUserName").ToString();
            var user = uRepo.GetByUsername(username);
            if(user != null)
            {
                data.Appointment.userID = user.ID;
                data.Appointment.isActive = true;
            }
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");
                var stringContent = new StringContent(JsonConvert.SerializeObject(data.Appointment), Encoding.UTF8, "application/json");
                var result = client.PostAsync("Appointment/AppointmentCreate", stringContent);
                result.Wait();
                if (result.IsCompletedSuccessfully)
                {
                    return RedirectToAction("AppointmentList", "Appointment");
                }
            }
            catch (Exception ex)
            {
                return NotFound("Randevu oluşturma işlemi sırasında hata oluştu! Hata: " + ex);
            }
            return NotFound("Randevu oluşturma işlemi sırasında hata oluştu!");
        }
        public IActionResult UpdateAppointment(int id)
        {
            int Role = 0;
            string Username = null;
            if (HttpContext.Session.GetInt32("userRole") == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            else
            {
                try
                {
                    Role = HttpContext.Session.GetInt32("userRole").Value;
                    Username = HttpContext.Session.GetString("loggedUserName").ToString();
                    var user = uRepo.GetByUsername(Username);
                    var appo = repo.GetByID(id);

                    if (user != null && (appo != null && appo.userID == user.ID))
                    {
                        AppointmentVM model = new AppointmentVM();
                        model.carList = carRepo.UserCarList(user.ID);
                        model.transmissionTypeList = carRepo.GetTransmissionTypeList();
                        model.fuelTypeList = carRepo.GetFuelTypeList();
                        model.userList = uRepo.GetTechnicianList();
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync("Appointment/" + id);
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var getAppo = readTask.Result;
                            model.Appointment = JsonConvert.DeserializeObject<Appointment>(getAppo);
                            return View(model);
                        }
                    }
                    else
                    {
                        return RedirectToAction("AppointmentList", "Appointment");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Randevu bilgilerinin görüntülenmesi işlemi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult UpdateAppointment(AppointmentVM data)
        {
            int Role = 0;
            string Username = null;
            if (HttpContext.Session.GetInt32("userRole") == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            else
            {
                try
                {
                    Role = HttpContext.Session.GetInt32("userRole").Value;
                    Username = HttpContext.Session.GetString("loggedUserName").ToString();
                    var user = uRepo.GetByUsername(Username);
                    var appo = repo.GetByID(data.Appointment.ID);
                    if (user != null && (appo != null && appo.userID == user.ID))
                    {
                        data.Appointment.userID = user.ID;
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var stringContent = new StringContent(JsonConvert.SerializeObject(data.Appointment), Encoding.UTF8, "application/json");
                        var result = client.PostAsync("Appointment/AppointmentUpdate", stringContent);
                        result.Wait();
                        if (result.IsCompletedSuccessfully)
                        {
                            return RedirectToAction("AppointmentList", "Appointment");
                        }
                    }
                    else
                    {
                        return RedirectToAction("IndexUserView", "Car");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Randevunun güncellenmesi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return NotFound("Güncelleme yapılacak randevu bulunamadı!");
        }
        public IActionResult DeleteAppointment(int id)
        {
            int Role = 0;
            string Username = null;
            if (HttpContext.Session.GetInt32("userRole") == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            else
            {
                try
                {
                    Role = HttpContext.Session.GetInt32("userRole").Value;
                    Username = HttpContext.Session.GetString("loggedUserName").ToString();
                    var user = uRepo.GetByUsername(Username);
                    var appo = repo.GetByID(id);
                    if (user != null && (appo != null && appo.userID == user.ID))
                    {
                        AppointmentVM model = new AppointmentVM();
                        model.carList = carRepo.UserCarList(user.ID);
                        model.transmissionTypeList = carRepo.GetTransmissionTypeList();
                        model.fuelTypeList = carRepo.GetFuelTypeList();
                        model.userList = uRepo.GetTechnicianList();
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync("Appointment/" + id);
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var getAppo = readTask.Result;
                            model.Appointment = JsonConvert.DeserializeObject<Appointment>(getAppo);
                            return View(model);
                        }
                    }
                    else
                    {
                        return RedirectToAction("AppointmentList", "Appointment");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Randevu bilgilerinin silinmesi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return View();
        }
        [HttpPost]      
        public IActionResult DeleteAppointment(AppointmentVM data)
        {
            int Role = 0;
            string Username = null;
            if (HttpContext.Session.GetInt32("userRole") == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            else
            {
                try
                {
                    Role = HttpContext.Session.GetInt32("userRole").Value;
                    Username = HttpContext.Session.GetString("loggedUserName").ToString();
                    var user = uRepo.GetByUsername(Username);
                    var appo = repo.GetByID(data.Appointment.ID);
                    if (user != null && (appo != null && appo.userID == user.ID))
                    {
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var result = client.GetAsync("Appointment/AppointmentDelete/" + data.Appointment.ID);
                        result.Wait();
                        if (result.IsCompletedSuccessfully)
                        {
                            return RedirectToAction("AppointmentList", "Appointment");
                        }
                    }
                    else
                    {
                        return RedirectToAction("IndexUserView", "Car");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Randevu bilgilerinin silinmesi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return NotFound("Randevu bilgilerinin silinmesi sırasında hata oluştu!");
        }
        [HttpGet]       
        public IActionResult AppointmentDetails(int id)
        {
            AppointmentVM model = new AppointmentVM();
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");
                var response = client.GetAsync("Appointment/" + id);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    var appo = readTask.Result;
                    model.Appointment = JsonConvert.DeserializeObject<Appointment>(appo);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Randevu detaylarının görüntülenmesi sırasında hata oluştu! Hata: " + ex);
            }
            return View();
        }
    }
}
