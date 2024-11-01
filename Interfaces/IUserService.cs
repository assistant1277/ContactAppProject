using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactAppProject.Models;

namespace ContactAppProject.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUser(int userId);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
    }
}