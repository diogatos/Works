using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class Bank
    {
        public List<Customer> AllCustomers { get; set; }

        public Bank()
        {
            AllCustomers = new List<Customer>();
        }

       

    }
}
