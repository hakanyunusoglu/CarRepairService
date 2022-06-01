using CarRepairService.API.DTO;
using CarRepairService.Business.Abstract;
using CarRepairService.Entities.DBO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarRepairService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServices repo;
        public UserController(IUserServices _repo)
        {
            repo = _repo;

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(UserVM data)
        {
            var user = repo.GetByUsername(data.Username);
            if (user != null)
            {
                try
                {
                    byte[] passwordHash, passwordSalt;
                    repo.CreatePasswordHash(data.Password, out passwordHash, out passwordSalt);
                    if (repo.VerifyPassword(data.Password, data.Username, data.passwordHash, data.passwordSalt))
                    {
                        return Ok(data);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Giriş esnasında bir hata oluştu! Hata :" + ex);
                }
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult GetActiveUserList()
        {
            var userList = repo.GetList();
            if (userList != null)
            {
                return Ok(userList);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet("[action]")]
        public IActionResult GetAllUserList()
        {
            var userList = repo.GetAllList();
            if (userList != null)
            {
                return Ok(userList);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            var user = repo.GetByID(id);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("[action]")]
        public IActionResult UserUpdate(UserVM model)
        {
            if (repo.GetByID(model.ID) != null)
            {
                User user = new User();
                try
                {
                    user.ID = model.ID;
                    user.isActive = model.isActive;
                    user.firstName = model.firstName;
                    user.lastName = model.lastName;
                    user.Description = model.Description;
                    user.Role = model.Role;
                    user.Username = model.Username;
                    user.passwordSalt = model.passwordSalt;
                    user.passwordHash = model.passwordHash;
                    repo.Update(user);
                }
                catch (Exception ex)
                {
                    return NotFound("Güncelleme işlemi sırasında hata oluştu! Hata :" + ex);
                }
                return Ok(user);
            }
            else
            {
                return NotFound("Güncelleme yapılacak kullanıcı bulunamadı!");
            }
        }
        [HttpGet("[action]/{id}")]
        public IActionResult UserDelete(int id)
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
                return NotFound("Silinecek kullanıcı bulunamadı!");
            }
        }
        [HttpPost("[action]")]
        public IActionResult UserCreate(UserVM model)
        {
            if (ModelState.IsValid)
            {
                byte[] passwordHash, passwordSalt;
                User user = new User();
                try
                {
                    user.ID = model.ID;
                    if (model.Role == 0)
                    {
                        user.isActive = true;
                        user.Role = 1;
                    }
                    else
                    {
                        user.isActive = model.isActive;
                        user.Role = model.Role;
                    }
                    user.firstName = model.firstName;
                    user.lastName = model.lastName;
                    user.Description = model.Description;
                    user.Username = model.Username;
                    repo.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                    user.passwordHash = passwordHash;
                    user.passwordSalt = passwordSalt;
                    repo.Add(user);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return NotFound("Kullanıcı kayıt işlemi sırasında bir hata oluştu! Hata :" + ex);
                }
            }
            return BadRequest(ModelState);
        }

    }
}