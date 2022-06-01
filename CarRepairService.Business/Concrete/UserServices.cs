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
    public class UserServices : IUserServices
    {
        private IUserRepository repo;
        public UserServices(IUserRepository _repo)
        {
            repo = _repo;
        }
        public User Add(User data)
        {
            return repo.Add(data);
        }

        public void Appointments()
        {
            throw new NotImplementedException();
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            repo.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public List<User> GetAllList()
        {
           return repo.GetAllList();
        }

        public User GetByID(int id)
        {
            return  repo.GetByID(id);
        }

        public User GetByUsername(string data)
        {
            return repo.GetByUsername(data);
        }

        public List<User> GetList()
        {
            return repo.GetList();
        }

        public List<User> GetTechnicianList()
        {
            return repo.GetTechnicianList();
        }

        public User Update(User data)
        {
            return repo.Update(data);
        }

        public bool VerifyPassword(string password, string username, byte[] passwordHash, byte[] passwordSalt)
        {
            return repo.VerifyPassword(password, username, passwordHash, passwordSalt);
        }
    }
}
