using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppProject.Models
{
    public class ContactDetail
    {
        public int ContactDetailId { get; set; }
        public int ContactId { get; set; }
        public string DetailType { get; set; }
        public string DetailValue { get; set; }
        public bool IsActive { get; set; } = true; 
    }
}