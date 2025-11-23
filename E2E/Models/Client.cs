using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2E.Models
{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipOrPostalCode { get; set; }

        public Client(string firstName, string lastName, string zipOrPostalCode)
        {
            FirstName = firstName;
            LastName = lastName;
            ZipOrPostalCode = zipOrPostalCode;
        }
    }
}
