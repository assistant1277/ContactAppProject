using ContactAppProject.Enums;
using ContactAppProject.Exceptions;
using ContactAppProject.Interfaces;
using ContactAppProject.Models;
using ContactAppProject.Services;
using System.Collections.Generic;

namespace ContactAppProject.Controllers
{
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public List<User> GetAllUsers() => _userService.GetAllUsers();

        public User GetUser(int userId)
        {
            var user = _userService.GetUser(userId);
            if (user == null)
            {
                throw new EntityNotFoundException("\nUser not found");
            }
            if (!user.IsActive)
            {
                throw new InactiveUserException("\nUser is inactive");
            }
            return user;
        }

        public void AddUser(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || user.UserName.Length < 3 || user.UserName.Length > 20)
            {
                throw new InvalidInputException("\nInvalid username format and username must be 3-20 characters long");
            }

            if (!Enum.IsDefined(typeof(UserRole), user.Role))
            {
                throw new InvalidInputException("\nInvalid user role specified");
            }
            _userService.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            var existingUser = GetUser(user.UserId);
            if (string.IsNullOrEmpty(user.UserName) || user.UserName.Length < 3 || user.UserName.Length > 20)
            {
                throw new InvalidInputException("\nInvalid username format and username must be 3-20 characters long");
            }

            if (!Enum.IsDefined(typeof(UserRole), user.Role))
            {
                throw new InvalidInputException("\nInvalid user role specified");
            }
            _userService.UpdateUser(user);
        }

        public void DeleteUser(int userId)
        {
            var user = GetUser(userId);
            if (string.IsNullOrEmpty(user.UserName) || user.UserName.Length < 3 || user.UserName.Length > 20)
            {
                throw new InvalidInputException("\nInvalid username format and username must be 3-20 characters long");
            }

            if (!Enum.IsDefined(typeof(UserRole), user.Role))
            {
                throw new InvalidInputException("\nInvalid user role specified");
            }
            _userService.DeleteUser(userId);
        }
    }
}