using ContactAppProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; } = true;
        public Enums.UserRole Role { get; set; }
    }
}