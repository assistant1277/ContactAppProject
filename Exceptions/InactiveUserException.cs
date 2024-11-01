using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppProject.Exceptions
{
    public class InactiveUserException:Exception
    {
        public InactiveUserException(string message) : base(message) { }
    }
}
