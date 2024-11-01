using ContactAppProject.Controllers;
using ContactAppProject.Enums;
using ContactAppProject.Exceptions;
using ContactAppProject.Interfaces;
using ContactAppProject.Models;
using ContactAppProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppProject.Presentations
{
    public class MainMenu
    {
        private readonly UserController _userController;
        private readonly ContactController _contactController;

        public MainMenu(UserController userController, ContactController contactController)
        {
            _userController = userController;
            _contactController = contactController;
        }

        public void Show()
        {
            int userId = GetUserId();
            if (userId == -1)
            {
                return;
            }
            try
            {
                var user = _userController.GetUser(userId);
                ShowMenuForUserRole(user); 
            }
            catch (EntityNotFoundException)
            {
                Console.WriteLine("\nUser id not found and enter valid user id");
            }
            catch (InactiveUserException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ShowMenuForUserRole(User user)
        {
            if (user.Role == UserRole.ADMIN)
            {
                var adminMenu = new AdminMenu(_userController);
                Console.WriteLine($"\nUser id exists and user is active-> {user.IsActive} and\nuser is {user.Role} \nand now redirecting to submenu 1");
                adminMenu.ShowAdminMenu();
            }
            else
            {
                var staffMenu = new StaffMenu(_contactController);
                Console.WriteLine($"\nUser id exists and user is active ->{user.IsActive} and\nuser is not admin it is {user.Role} \nand now redirecting to submenu 2");
                staffMenu.ShowStaffMenu();
            }
        }

        private int GetUserId()
        {
            Console.Write("Enter user id -> ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Invalid user id format and enter valid number");
                return -1;
            }
            return userId;
        }

        public static void Start()
        {
            Console.WriteLine("***** Welcome to Contact App *****\n");
            var userService = new UserService();
            var contactService = new ContactService();
            var userController = new UserController(userService);
            var contactController = new ContactController(contactService);
            var mainMenu = new MainMenu(userController, contactController);
            mainMenu.Show();
        }
    }
}