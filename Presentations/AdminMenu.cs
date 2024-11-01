using ContactAppProject.Controllers;
using ContactAppProject.Enums;
using ContactAppProject.Exceptions;
using ContactAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = ContactAppProject.Exceptions.ValidationException;
namespace ContactAppProject.Presentations
{
    public class AdminMenu
    {
        private readonly UserController _userController;

        public AdminMenu(UserController userController)
        {
            _userController=userController;
        }

        public void ShowAdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\nAdmin menu ->");
                Console.WriteLine("1)Add new user");
                Console.WriteLine("2)Modify existing user");
                Console.WriteLine("3)Delete user (soft)");
                Console.WriteLine("4)Display all users");
                Console.WriteLine("5)Find user by id");
                Console.WriteLine("6)Logout");
                Console.Write("\nChoose option -> ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddUser();
                        break;
                    case "2":
                        UpdateUser();
                        break;
                    case "3":
                        DeleteUser();
                        break;
                    case "4":
                        DisplayAllUsers();
                        break;
                    case "5":
                        FindUserById();
                        break;
                    case "6":
                        Console.WriteLine("Logging out\n");
                        MainMenu.Start();
                        return;
                    default:
                        Console.WriteLine("\nInvalid option try again\n");
                        break;
                }
            }
        }
       
        private void AddUser()
        {
            while (true) 
            {
                try
                {
                    Console.Write("Enter username -> ");
                    string userName = Console.ReadLine();
                    Console.WriteLine("Choose a role ->");
                    Console.WriteLine("1)Staff");
                    Console.WriteLine("2)Admin");
                    Console.Write("Select role -> ");
                    string roleInput = Console.ReadLine();

                    UserRole userRole;
                    switch (roleInput)
                    {
                        case "1":
                            userRole= UserRole.STAFF;
                            break;
                        case "2":
                            userRole=UserRole.ADMIN; 
                            break;
                        default:
                            Console.WriteLine("\nInvalid role selection and defaulting to staff");
                            userRole = UserRole.STAFF;
                            break;
                    }

                    var user =new User{UserName=userName,Role=userRole};
                    _userController.AddUser(user);
                    Console.WriteLine("\nUser added successfully");
                    break;
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch(InvalidInputException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }

                if (!AskToContinue()) break;
            }
        }

        private void UpdateUser()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter user id to update -> ");
                    int userId =int.Parse(Console.ReadLine());
                    var user= _userController.GetUser(userId);

                    Console.Write("Enter new username -> ");
                    string newUserName = Console.ReadLine();
                    user.UserName=newUserName;

                    Console.Write("\nDo you want to change user role if yes press 'y' if no press 'n' -> ");
                    string changeRoleInput = Console.ReadLine();

                    //check if user enter 'y' to confirm their choice and
                    //ignoring whether they typed it in uppercase or lowercase
                    //so both 'y' and 'Y' will be treated as same answer
                    if (changeRoleInput.Equals("y",StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Choose new role ->");
                        Console.WriteLine("1)Staff");
                        Console.WriteLine("2)Admin");
                        Console.Write("Select role -> ");
                        string roleInput= Console.ReadLine();

                        UserRole newUserRole;
                        switch (roleInput)
                        {
                            case "1":
                                newUserRole=UserRole.STAFF;
                                break;
                            case "2":
                                newUserRole =UserRole.ADMIN; 
                                break;
                            default:
                                Console.WriteLine("\nInvalid role selection and role will not be changed");
                                newUserRole =user.Role; 
                                break;
                        }
                        user.Role=newUserRole;
                    }
                    _userController.UpdateUser(user);
                    Console.WriteLine("\nUser updated successfully");
                    break;
                }
                catch (EntityNotFoundException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                if (!AskToContinue()) break;
            }
        }

        private void DeleteUser()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter user id to delete -> ");
                    int userId =int.Parse(Console.ReadLine());
                    _userController.DeleteUser(userId);
                    Console.WriteLine("\nUser deleted successfully");
                    break;
                }
                catch (EntityNotFoundException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }

                if (!AskToContinue()) break;
            }
        }

        private void DisplayAllUsers()
        {
            var users= _userController.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"\nid -> {user.UserId}, name -> {user.UserName}, role -> {user.Role}, active -> {user.IsActive}");
            }
        }

        private void FindUserById()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter user id to find -> ");
                    int userId =int.Parse(Console.ReadLine());
                    var user=_userController.GetUser(userId);
                    Console.WriteLine($"\nFound user -> id -> {user.UserId}, name -> {user.UserName}, role -> {user.Role}, active -> {user.IsActive}");
                    break;
                }
                catch (EntityNotFoundException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }

                if (!AskToContinue()) break;
            }
        }

        private bool AskToContinue()
        {
            Console.Write("\nDo you want to try again if yes press 'y' if no press 'n' -> ");
            string input=Console.ReadLine();
            return input.ToLower() == "y";
        }
    }
}