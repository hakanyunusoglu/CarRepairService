using CarRepairService.Entities.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.Business.Abstract
{
    public interface IUserServices
    {
        User Add(User data);
        User Update(User data);
        void Delete(int id);
        List<User> GetList();
        List<User> GetAllList();
        User GetByID(int id);
        User GetByUsername(string data);
        void Appointments();
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPassword(string password, string username, byte[] passwordHash, byte[] passwordSalt);
        List<User> GetTechnicianList();
    }
}
