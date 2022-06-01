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
    public class CarController : Controller
    {
        private readonly ICarServices repo;
        private readonly IUserServices uRepo;
        public CarController(ICarServices _repo, IUserServices _uRepo)
        {
            repo = _repo;
            uRepo = _uRepo;
        }
        [HttpGet]
        public IActionResult IndexUserView()
        {
            CarVM model = new CarVM();
            string Username;
            int userID = 0;
            try
            {
                if (HttpContext.Session.GetInt32("userRole") != null)
                {
                    Username = HttpContext.Session.GetString("loggedUserName").ToString();
                    var user = uRepo.GetByUsername(Username);
                    if (user == null)
                    {
                        ViewBag.NotLogged = "notLogged";
                    }
                    else
                    {
                        userID = user.ID;
                    }
                }
                else
                {
                    ViewBag.NotLogged = "notLogged";
                }
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44371/api/");
                var response = client.GetAsync($"Car/GetActiveCarList/{userID}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    var car = readTask.Result;
                    model.carList = JsonConvert.DeserializeObject<List<Car>>(car);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Araçları listeleme işlemi sırasında hata oluştu! Hata: " + ex);
            }
            return View();
        }
        public IActionResult AllCarIndex()
        {
            int Role = 0;
            if (HttpContext.Session.GetInt32("userRole") == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            else
            {
                Role = HttpContext.Session.GetInt32("userRole").Value;
                if (Role == 3)
                {
                    CarVM model = new CarVM();
                    HttpClient client = new HttpClient();
                    try
                    {
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync("Car/GetAllCarList");
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var car = readTask.Result;
                            model.carList = JsonConvert.DeserializeObject<List<Car>>(car);
                            return View(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        return NotFound("Araç listeleme işlemi sırasında hata oluştu! Hata: " + ex);
                    }
                }
                else
                {
                    return RedirectToAction("IndexUserView", "Car");
                }
            }
            return View();
        }
        public IActionResult ActiveCarIndex()
        {
            int Role = 0;
            if (HttpContext.Session.GetInt32("userRole") == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            else
            {
                Role = HttpContext.Session.GetInt32("userRole").Value;
                if (Role == 3)
                {
                    CarVM model = new CarVM();
                    HttpClient client = new HttpClient();
                    try
                    {
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync("Car/GetActiveCarList");
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var car = readTask.Result;
                            model.carList = JsonConvert.DeserializeObject<List<Car>>(car);
                            return View(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        return NotFound("Araç listeleme işlemi sırasında hata oluştu! Hata: " + ex);
                    }
                }
                else
                {
                    return RedirectToAction("IndexUserView", "Car");
                }
            }
            return View();
        }
        public IActionResult CreateCar()
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
                        CarVM carsVM = new CarVM();
                        carsVM.carList = repo.GetList();
                        carsVM.transmissionTypeList = repo.GetTransmissionTypeList();
                        carsVM.fuelTypeList = repo.GetFuelTypeList();
                        carsVM.userList = uRepo.GetList();
                        return View(carsVM);
                    }
                    else
                    {
                        return RedirectToAction("IndexUserView", "Car");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Araç listeleme işlemi sırasında hata oluştu! Hata: " + ex);
                }
            }
        }
        [HttpPost]
        public IActionResult CreateCar(CarVM data)
        {
            string username = null;

            username = HttpContext.Session.GetString("loggedUserName").ToString();
            var user = uRepo.GetByUsername(username);
            if (data.Car.userID == 0)
            {
                if (user != null)
                {
                    data.Car.userID = user.ID;
                }
            }
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");
                var stringContent = new StringContent(JsonConvert.SerializeObject(data.Car), Encoding.UTF8, "application/json");
                var result = client.PostAsync("Car/CarCreate", stringContent);
                result.Wait();
                if (result.IsCompletedSuccessfully)
                {
                    return RedirectToAction("IndexUserView", "Car");
                }
            }
            catch (Exception ex)
            {
                return NotFound("Araç ekleme işlemi sırasında hata oluştu! Hata: " + ex);
            }
            return NotFound("Araç ekleme işlemi sırasında hata oluştu!");
        }
        public IActionResult CarUpdate(int id)
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
                    if (user != null && Role == 3)
                    {
                        CarVM model = new CarVM();
                        model.carList = repo.GetList();
                        model.transmissionTypeList = repo.GetTransmissionTypeList();
                        model.fuelTypeList = repo.GetFuelTypeList();
                        model.userList = uRepo.GetList();
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync("Car/" + id);
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var getCar = readTask.Result;
                            model.Car = JsonConvert.DeserializeObject<Car>(getCar);
                            return View(model);
                        }
                    }
                    else
                    {
                        return RedirectToAction("IndexUserView", "Car");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Araç bilgilerinin görüntülenmesi işlemi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult CarUpdate(CarVM data)
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
                    if (user != null && Role == 3)
                    {
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var stringContent = new StringContent(JsonConvert.SerializeObject(data.Car), Encoding.UTF8, "application/json");
                        var result = client.PostAsync("Car/CarUpdate", stringContent);
                        result.Wait();
                        if (result.IsCompletedSuccessfully)
                        {
                            return RedirectToAction("AllCarIndex", "Car");
                        }
                    }
                    else
                    {
                        return RedirectToAction("IndexUserView", "Car");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Araç bilgilerinin güncellenmesi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return NotFound("Güncelleme yapılacak araç bulunamadı!");
        }
        public IActionResult CarDelete(int id)
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
                    if (user != null && Role == 3)
                    {
                        CarVM model = new CarVM();
                        model.carList = repo.GetList();
                        model.transmissionTypeList = repo.GetTransmissionTypeList();
                        model.fuelTypeList = repo.GetFuelTypeList();
                        model.userList = uRepo.GetList();
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync("Car/" + id);
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var getCar = readTask.Result;
                            model.Car = JsonConvert.DeserializeObject<Car>(getCar);
                            return View(model);
                        }
                    }
                    else
                    {
                        return RedirectToAction("IndexUserView", "Car");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Araç bilgilerinin silinmesi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult CarDelete(CarVM data)
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
                    if (user != null && Role == 3)
                    {
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var result = client.GetAsync("Car/CarDelete/" + data.Car.ID);
                        result.Wait();
                        if (result.IsCompletedSuccessfully)
                        {
                            return RedirectToAction("AllCarIndex", "Car");
                        }
                    }
                    else
                    {
                        return RedirectToAction("IndexUserView", "Car");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Araç bilgilerinin silinmesi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return NotFound("Araç bilgilerinin silinmesi sırasında hata oluştu!");
        }
        [HttpGet]
        public IActionResult CarDetails(int id)
        {
            CarVM model = new CarVM();
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");
                var response = client.GetAsync("Car/" + id);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    var car = readTask.Result;
                    model.Car = JsonConvert.DeserializeObject<Car>(car);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Araç detaylarının görüntülenmesi sırasında hata oluştu! Hata: " + ex);
            }
            return View();
        }
        public IActionResult UserCarList()
        {
            string Username = null;
            if (HttpContext.Session.GetInt32("userRole") == null)
            {
                return RedirectToAction("IndexUserView", "Car");
            }
            else
            {
                try
                {
                    Username = HttpContext.Session.GetString("loggedUserName").ToString();
                    var user = uRepo.GetByUsername(Username);
                    if (user != null)
                    {
                        CarVM model = new CarVM();
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync($"Car/GetUserCars/{user.ID}");
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var car = readTask.Result;
                            model.carList = JsonConvert.DeserializeObject<List<Car>>(car);
                            return View(model);
                        }
                    }
                    else
                    {
                        return RedirectToAction("IndexUserView", "Car");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Araçları listeleme işlemi sırasında hata oluştu! Hata: " + ex);
                }
                return View();
            }
        }
    }
}