using ContactAppProject.Enums;
using ContactAppProject.Interfaces;
using ContactAppProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactAppProject.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users;

        public UserService()
        {
            _users = new List<User>
            {
                new User { UserId = 1, UserName = "deepak mishra", Role = UserRole.ADMIN, IsActive = true },
                new User { UserId = 2, UserName = "sanket patil", Role = UserRole.STAFF, IsActive = true }
            };
        }

        public List<User> GetAllUsers()
        {
            return _users.ToList();
        }

        public User GetUser(int userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId); 
        }

        public void AddUser(User user)
        {
            user.UserId = _users.Max(u => u.UserId) + 1; 
            _users.Add(user);
        }

        public void UpdateUser(User updatedUser)
        {
            var user = GetUser(updatedUser.UserId);
            if (user != null)
            {
                user.UserName = updatedUser.UserName;
                user.Role = updatedUser.Role;
            }
        }

        public void DeleteUser(int userId)
        {
            var user = GetUser(userId);
            if (user != null)
                user.IsActive = false; 
        }
    }
}