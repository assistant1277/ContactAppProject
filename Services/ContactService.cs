using ContactAppProject.Enums;
using ContactAppProject.Interfaces;
using ContactAppProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactAppProject.Services
{
    public class ContactService : IContactService
    {
        private readonly List<Contact> _contacts;
        private readonly List<ContactDetail> _contactDetails;

        public ContactService()
        {
            _contacts = new List<Contact>
            {
                new Contact { ContactId = 1, ContactName = "raj kumar", IsActive = true },
                new Contact { ContactId = 2, ContactName = "sanket patil", IsActive = true }
            };

            _contactDetails = new List<ContactDetail>
            {
                new ContactDetail { ContactDetailId = 1, ContactId = 1, DetailType = "Email", DetailValue = "raj@gmail.com" },
                new ContactDetail { ContactDetailId = 1, ContactId = 1, DetailType = "Phone", DetailValue = "9655723119" },
                new ContactDetail { ContactDetailId = 2, ContactId = 2, DetailType = "Email", DetailValue = "sanket@gmail.com" }
            };
        }

        public List<Contact> GetAllContacts()
        {
            return _contacts.Where(c => c.IsActive).ToList();                             
        }

        public Contact GetContact(int contactId)
        {
            return _contacts.FirstOrDefault(c => c.ContactId == contactId && c.IsActive);
        }

        public void AddContact(Contact contact)
        {
            contact.ContactId = _contacts.Max(c => c.ContactId) + 1;
            _contacts.Add(contact);
        }

        public void UpdateContact(Contact updatedContact)
        {
            var existingContact = GetContact(updatedContact.ContactId);
            if (existingContact != null)
            {
                existingContact.ContactName = updatedContact.ContactName;
            }
        }

        public void DeleteContact(int contactId)
        {
            var contact = GetContact(contactId);
            if (contact != null)
            {
                contact.IsActive = false; 
            }
        }

        public List<ContactDetail> GetContactDetails(int contactId)
        {
            return _contactDetails.Where(cd => cd.ContactId == contactId).ToList();
        }

        public void AddContactDetail(ContactDetail contactDetail)
        {
            contactDetail.ContactDetailId = _contactDetails.Max(cd => cd.ContactDetailId) + 1;
            _contactDetails.Add(contactDetail);
        }

        public void UpdateContactDetail(ContactDetail updatedDetail)
        {
            var existingDetail = _contactDetails.FirstOrDefault(cd => cd.ContactDetailId == updatedDetail.ContactDetailId);
            if (existingDetail != null)
            {
                existingDetail.DetailType = updatedDetail.DetailType;
                existingDetail.DetailValue = updatedDetail.DetailValue;
            }
        }

        public void DeleteContactDetail(int detailId)
        {
            var contactDetail = _contactDetails.FirstOrDefault(cd => cd.ContactDetailId == detailId);
            if (contactDetail != null)
            {
                _contactDetails.Remove(contactDetail); 
            }
        }

        public ContactDetail GetContactDetail(int detailId)
        {
            return _contactDetails.FirstOrDefault(cd => cd.ContactDetailId == detailId);
        }
    }
}