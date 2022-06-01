using CarRepairService.Business.Abstract;
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
    public class UserController : Controller
    {
        private readonly IUserServices repo;
        public UserController(IUserServices _repo)
        {
            repo = _repo;
        }
        public IActionResult Index()
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
                    List<UserVM> model = new List<UserVM>();
                    HttpClient client = new HttpClient();
                    try
                    {
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync("User");
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var user = readTask.Result;
                            model = JsonConvert.DeserializeObject<List<UserVM>>(user);
                            return View(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        return NotFound("Kullanıcıları listeleme işlemi sırasında hata oluştu! Hata: " + ex);
                    }
                }
                else
                {
                    return RedirectToAction("IndexUserView", "Car");
                }
            }
            return View();
        }
        public IActionResult AllUserIndex()
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
                    List<UserVM> model = new List<UserVM>();
                    HttpClient client = new HttpClient();
                    try
                    {
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync("User/GetAllUserList");
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var user = readTask.Result;
                            model = JsonConvert.DeserializeObject<List<UserVM>>(user);
                            return View(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        return NotFound("Kullanıcıları listeleme işlemi sırasında hata oluştu! Hata: " + ex);
                    }
                }
                else
                {
                    return RedirectToAction("IndexUserView", "Car");
                }
            }
            return View();
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(UserVM data)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");
                var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var result = client.PostAsync("User/UserCreate", stringContent);
                result.Wait();
                if (result.IsCompletedSuccessfully)
                {
                    return RedirectToAction("IndexUserView", "Car");
                }
            }
            catch (Exception ex)
            {
                return NotFound("Ekleme işlemi sırasında hata oluştu! Hata: " + ex);
            }
            return NotFound("Ekleme işlemi sırasında hata oluştu!");
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(UserVM model)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");
                var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var result = client.PostAsync("User/SignIn", stringContent);
                result.Wait();
                if (result.Result.IsSuccessStatusCode)
                {
                    var findUser = repo.GetByUsername(model.Username);
                    HttpContext.Session.Clear();
                    HttpContext.Session.SetString("loggedUserName", model.Username);
                    HttpContext.Session.SetInt32("userRole", findUser.Role);
                    return RedirectToAction("IndexUserView", "Car");
                }
                else
                {
                    return RedirectToAction("SignIn", "User");
                }
            }
            catch (Exception ex)
            {
                return NotFound("Kullanıcı giriş işlemi sırasında hata oluştu! Hata: " + ex);
            }
        }
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn", "User");
        }
        public IActionResult UserUpdate(int id)
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
                    var user = repo.GetByUsername(Username);
                    if (user != null && Role == 3)
                    {
                        UserVM model = new UserVM();
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync("User/" + id);
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var getUser = readTask.Result;
                            model = JsonConvert.DeserializeObject<UserVM>(getUser);
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
                    return NotFound("Kullanıcı bilgilerinin görüntülenmesi işlemi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult UserUpdate(UserVM data)
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
                    var user = repo.GetByUsername(Username);
                    if (user != null && Role == 3)
                    {
                        if (string.IsNullOrEmpty(data.Password))
                        {
                            data.passwordHash = user.passwordHash;
                            data.passwordSalt = user.passwordSalt;
                        }
                        else if (user != null && !string.IsNullOrEmpty(data.Password))
                        {
                            byte[] passwordHash, passwordSalt;
                            repo.CreatePasswordHash(data.Password, out passwordHash, out passwordSalt);
                            data.passwordSalt = passwordHash;
                            data.passwordHash = passwordSalt;
                        }
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        var result = client.PostAsync("User/UserUpdate", stringContent);
                        result.Wait();
                        if (result.IsCompletedSuccessfully)
                        {
                            return RedirectToAction("AllUserIndex", "User");
                        }
                    }
                    else
                    {
                        return RedirectToAction("IndexUserView", "Car");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Kullanıcı bilgilerinin güncellenmesi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return NotFound("Güncelleme yapılacak kullanıcı bulunamadı!");
        }
        public IActionResult UserDelete(int id)
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
                    var user = repo.GetByUsername(Username);
                    if (user != null && Role == 3)
                    {
                        UserVM model = new UserVM();
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var response = client.GetAsync("User/" + id);
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            var getUser = readTask.Result;
                            model = JsonConvert.DeserializeObject<UserVM>(getUser);
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
                    return NotFound("Kullanıcı bilgilerinin silinmesi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult UserDelete(UserVM data)
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
                    var user = repo.GetByUsername(Username);
                    if (user != null && Role == 3)
                    {
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:44371/api/");
                        var result = client.GetAsync("User/UserDelete/" + data.ID);
                        result.Wait();
                        if (result.IsCompletedSuccessfully)
                        {
                            return RedirectToAction("AllUserIndex", "User");
                        }
                    }
                    else
                    {
                        return RedirectToAction("IndexUserView", "Car");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Kullanıcı bilgilerinin silinmesi sırasında hata oluştu! Hata: " + ex);
                }
            }
            return NotFound("Kullanıcı bilgilerinin silinmesi sırasında hata oluştu!");
        }
        [HttpGet]
        public IActionResult UserDetails(int id)
        {
            UserVM model = new UserVM();
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");
                var response = client.GetAsync("User/" + id);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    var user = readTask.Result;
                    model = JsonConvert.DeserializeObject<UserVM>(user);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Kullanıcı detaylarının görüntülenmesi sırasında hata oluştu! Hata: " + ex);
            }
            return View();
        }
    }
}
