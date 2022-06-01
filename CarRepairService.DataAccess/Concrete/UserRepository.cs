using CarRepairService.DataAccess.Abstract;
using CarRepairService.DataAccess.DataContext;
using CarRepairService.Entities.DBO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        public User Add(User data)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                carDbContext.Users.Add(data);
                carDbContext.SaveChanges();
                return data;
            }
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        { 
                var hmac = new HMACSHA512();
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public void Delete(int id)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                var user = GetByID(id);
                if (user != null)
                {
                    user.isActive = false;
                    carDbContext.Users.Update(user);
                }
                carDbContext.SaveChanges();
            }
        }

        public User GetByID(int id)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Users.FirstOrDefault(x => x.ID == id);
            }
        }

        public User GetByUsername(string data)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Users.FirstOrDefault(x => x.Username == data);
            }
        }

        public List<User> GetList()
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Users.Where(x=>x.isActive == true).ToList();
            }
        }

        public void Appointments()
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                throw new NotImplementedException();
            }
        }

        public User Update(User data)
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                carDbContext.Users.Update(data);
                carDbContext.SaveChanges();
                return data;
            }
        }

        public bool VerifyPassword(string password, string username, byte[] passwordHash, byte[] passwordSalt)
        {
                var findUser = GetByUsername(username);
                var hmac = new HMACSHA512(findUser.passwordSalt);
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != findUser.passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
        }

        public List<User> GetAllList()
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Users.ToList();
            }
        }

        public List<User> GetTechnicianList()
        {
            using (var carDbContext = new CarRepairServiceDataContext())
            {
                return carDbContext.Users.Where(x => x.isActive == true && x.Role == 2).ToList();
            }
        }
    }
}
