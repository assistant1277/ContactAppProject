using ContactAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppProject.Interfaces
{
    public interface IContactService
    {
        List<Contact> GetAllContacts();
        Contact GetContact(int contactId);
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(int contactId);
        List<ContactDetail> GetContactDetails(int contactId);
        ContactDetail GetContactDetail(int detailId); 
        void AddContactDetail(ContactDetail contactDetail);
        void UpdateContactDetail(ContactDetail contactDetail);
        void DeleteContactDetail(int detailId);
    }
}