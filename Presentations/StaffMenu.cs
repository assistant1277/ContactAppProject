using ContactAppProject.Controllers;
using ContactAppProject.Exceptions;
using ContactAppProject.Interfaces;
using ContactAppProject.Models;
using ContactAppProject.Services;

namespace ContactAppProject.Presentations
{
    public class StaffMenu
    {
        private readonly ContactController _contactController;
        public StaffMenu(ContactController contactController)
        {
            _contactController = contactController;
        }

        public void ShowStaffMenu()
        {
            while (true)
            {
                Console.WriteLine("\nStaff menu ->");
                Console.WriteLine("1)Work on contacts");
                Console.WriteLine("2)Work on contact details");
                Console.WriteLine("3)Logout");
                Console.Write("Choose option -> ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ContactSubmenu();
                        break;
                    case "2":
                        ContactDetailSubmenu();
                        break;
                    case "3":
                        Console.WriteLine("Logging out\n");
                        MainMenu.Start();
                        return;
                    default:
                        Console.WriteLine("\nInvalid option try again");
                        break;
                }
            }
        }

        private void ContactSubmenu()
        {
            while (true)
            {
                Console.WriteLine("\nContacts menu ->");
                Console.WriteLine("1)Add new contact");
                Console.WriteLine("2)Modify existing contact");
                Console.WriteLine("3)Delete contact (soft)");
                Console.WriteLine("4)Display all contacts");
                Console.WriteLine("5)Find contact by id");
                Console.WriteLine("6)Return to staff menu");
                Console.Write("Choose option -> ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddContact();
                        break;
                    case "2":
                        UpdateContact();
                        break;
                    case "3":
                        DeleteContact();
                        break;
                    case "4":
                        DisplayAllContacts();
                        break;
                    case "5":
                        FindContactById();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("\nInvalid option try again");
                        break;
                }
            }
        }

        private void AddContact()
        {
            while (true) 
            {
                try
                {
                    Console.Write("Enter contact name -> ");
                    string contactName = Console.ReadLine();
                    var contact = new Contact { ContactName = contactName };
                    _contactController.AddContact(contact);
                    Console.WriteLine("\nContact added successfully");
                    break; 
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }

                if (!AskToContinue()) break;
            }
        }

        private void UpdateContact()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter contact id to update -> ");
                    int contactId = int.Parse(Console.ReadLine());
                    var contact = _contactController.GetContact(contactId);
                    Console.Write("Enter new contact name -> ");
                    contact.ContactName = Console.ReadLine();
                    _contactController.UpdateContact(contact);
                    Console.WriteLine("\nContact updated successfully");
                    break;
                }
                catch (EntityNotFoundException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine($"\nValidation error -> {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }

                if (!AskToContinue()) break;
            }
        }

        private void DeleteContact()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter contact id to delete -> ");
                    int contactId = int.Parse(Console.ReadLine());
                    _contactController.DeleteContact(contactId);
                    Console.WriteLine("\nContact deleted successfully");
                    break;
                }
                catch (EntityNotFoundException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }

                if (!AskToContinue()) break;
            }
        }
        
        private void DisplayAllContacts()
        {
            var contacts = _contactController.GetAllContacts();
            foreach (var contact in contacts)
            {
                Console.WriteLine($"\nContact id -> {contact.ContactId}, name -> {contact.ContactName}, active -> {contact.IsActive}");
            }
        }

        private void FindContactById()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter contact id -> ");
                    int contactId = int.Parse(Console.ReadLine());
                    var contact = _contactController.GetContact(contactId);
                    Console.WriteLine($"\nFound contact and id -> {contact.ContactId}, name -> {contact.ContactName}, active -> {contact.IsActive}");
                    break;
                }
                catch (EntityNotFoundException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }

                if (!AskToContinue()) break;
            }
        }

        private void ContactDetailSubmenu()
        {
            while (true)
            {
                Console.WriteLine("\nContact Details menu ->");
                Console.WriteLine("1)Add new contact detail");
                Console.WriteLine("2)Modify existing contact detail");
                Console.WriteLine("3)Delete contact detail");
                Console.WriteLine("4)Display all contact details for a contact");
                Console.WriteLine("5)Return to staff menu");
                Console.Write("Choose option -> ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddContactDetail();
                        break;
                    case "2":
                        UpdateContactDetail();
                        break;
                    case "3":
                        DeleteContactDetail();
                        break;
                    case "4":
                        DisplayContactDetails();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("\nInvalid option try again");
                        break;
                }
            }
        }

        private void AddContactDetail()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter contact id for detail -> ");
                    int contactId = int.Parse(Console.ReadLine());

                    Console.Write("Enter contact name for verification -> ");
                    string contactName = Console.ReadLine();

                    Console.Write("Enter detail type like (email,phone) -> ");
                    string detailType = Console.ReadLine();
                    Console.Write("Enter detail value -> ");
                    string detailValue = Console.ReadLine();

                    var contactDetail = new ContactDetail
                    {
                        ContactId = contactId,
                        DetailType = detailType,
                        DetailValue = detailValue
                    };

                    _contactController.AddContactDetail(contactId, contactName, contactDetail);

                    Console.WriteLine("\nContact detail added successfully");
                    break;
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch (EntityNotFoundException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }

                if (!AskToContinue()) break;
            }
        }

        private void UpdateContactDetail()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter detail id to update -> ");
                    int detailId = int.Parse(Console.ReadLine());
                    var contactDetail = _contactController.GetContactDetail(detailId);
                    Console.Write("Enter new detail type like (email,phone) -> ");
                    contactDetail.DetailType = Console.ReadLine();
                    Console.Write("Enter new detail value -> ");
                    contactDetail.DetailValue = Console.ReadLine();
                    _contactController.UpdateContactDetail(contactDetail);
                    Console.WriteLine("\nContact detail updated successfully");
                }
                catch (EntityNotFoundException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine($"\nValidation error -> {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                if (!AskToContinue()) break;
            }
        }

        private void DeleteContactDetail()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter detail id to delete -> ");
                    int detailId = int.Parse(Console.ReadLine());
                    _contactController.DeleteContactDetail(detailId);
                    Console.WriteLine("\nContact detail deleted successfully");
                }
                catch (EntityNotFoundException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                if (!AskToContinue()) break;
            }
        }

        private void DisplayContactDetails()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter contact id to display details -> ");
                    int contactId = int.Parse(Console.ReadLine());

                    var contact = _contactController.GetContact(contactId);
                    Console.WriteLine($"\nContact information ->");
                    Console.WriteLine($"\nContact id -> {contact.ContactId}, name -> {contact.ContactName}, active -> {contact.IsActive}");

                    var contactDetails = _contactController.GetContactDetails(contactId);

                    if (contactDetails.Any())
                    {
                        foreach (var detail in contactDetails)
                        {
                            Console.WriteLine($"\nDetail id -> {detail.ContactDetailId}, type -> {detail.DetailType}, value -> {detail.DetailValue}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo contact details available for this contact");
                    }
                    break; 
                }
                catch (EntityNotFoundException ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError -> {ex.Message}");
                }

                if (!AskToContinue()) break;
            }
        }

        private bool AskToContinue()
        {
            Console.Write("\nDo you want to try again if yes press 'y' if no press 'n' -> ");
            string input = Console.ReadLine();
            return input.ToLower() == "y";
        }
    }
}