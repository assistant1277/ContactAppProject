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
using ValidationException = ContactAppProject.Exceptions.ValidationException;

namespace ContactAppProject.Controllers
{

    public class ContactController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public List<Contact> GetAllContacts() => _contactService.GetAllContacts();

        public Contact GetContact(int contactId)
        {
            var contact = _contactService.GetContact(contactId);
            if (contact == null)
            {
                throw new EntityNotFoundException("\nContact not found");
            }
            return contact;
        }

        public void AddContact(Contact contact)
        {
            //check if there is already contact with same name as new contact name
            //and ignoring uppercase or lowercase difference in name comparison
            //and it will prevent adding new contact with duplicate name
            //and if such contact already exists then throw error to let user know
            //.Any() ->check if there is any contact in list that matches condition inside parentheses
            // and c -> is shorthand way of saying for each contact in list and
            //c.ContactName.Equals(contact.ContactName,StringComparison.OrdinalIgnoreCase) -> check if ContactName of any existing contact
            //c.ContactName matches ContactName of new contact being added contact.ContactName
            // and Equals(StringComparison.OrdinalIgnoreCase) compares names in case insensitive style so Om and om would be considered equal
            if (_contactService.GetAllContacts().Any(c => c.ContactName.Equals(contact.ContactName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidInputException("\ncontact with this name already exists");
            }

            //check if contact name is either empty or actually number
            //and if either of these conditions is true show error message and
            //int.TryParse tries to convert text in ContactName to number integer
            //out _ part is where it would normally store result if it were number but since we dont need it here then _ is used as placeholder
            //and if ContactName is number and TryParse return true otherwise it return false
            if (string.IsNullOrEmpty(contact.ContactName) || int.TryParse(contact.ContactName, out _))
            {
                throw new InvalidInputException("\nContact name cannot be empty and enter valid name not number");
            }
            _contactService.AddContact(contact);
        }

        public void UpdateContact(Contact contact)
        {
            var existingContact = _contactService.GetContact(contact.ContactId);
            if (existingContact == null)
            {
                throw new EntityNotFoundException("\nContact not found");
            }
            if (!existingContact.IsActive)
            {
                throw new ValidationException("\nCannot update inactive contact");
            }
            _contactService.UpdateContact(contact);
        }

        public void DeleteContact(int contactId)
        {
            var contact = _contactService.GetContact(contactId);
            if (contact == null)
            {
                throw new EntityNotFoundException("\nContact not found");
            }
            _contactService.DeleteContact(contactId);
        }

        public List<ContactDetail> GetContactDetails(int contactId)
        {
            return _contactService.GetContactDetails(contactId);
        }

        public void AddContactDetail(int contactId, string contactName, ContactDetail contactDetail)
        {
            var contact = _contactService.GetContact(contactId);
            if (contact == null)
            {
                throw new EntityNotFoundException("\nContact not found with this id");
            }

            if (!contact.ContactName.Equals(contactName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidInputException("\nContact name does not match with your provided id");
            }

            if (string.IsNullOrEmpty(contactDetail.DetailValue))
            {
                throw new InvalidInputException("\nDetail value cannot be empty");
            }
            _contactService.AddContactDetail(contactDetail);
        }


        public void UpdateContactDetail(ContactDetail contactDetail)
        {
            var existingDetail = _contactService.GetContactDetails(contactDetail.ContactDetailId);
            if (existingDetail == null)
            {
                throw new EntityNotFoundException("\nContact detail not found");
            }
            _contactService.UpdateContactDetail(contactDetail);
        }

        public void DeleteContactDetail(int detailId)
        {
            var contactDetail = _contactService.GetContactDetails(detailId);
            if (contactDetail == null)
            {
                throw new EntityNotFoundException("\nContact detail not found");
            }
            _contactService.DeleteContactDetail(detailId);
        }

        public ContactDetail GetContactDetail(int detailId)
        {
            var contactDetail = _contactService.GetContactDetail(detailId);
            if (contactDetail == null)
            {
                throw new EntityNotFoundException("\nContact detail not found");
            }
            return contactDetail;
        }
    }
}