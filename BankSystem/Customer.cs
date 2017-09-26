using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class Customer
    {

        public Customer(int id)
        {
            ID = id;
            Accounts = new List<Account>();
        }

        public int ID { get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Account> Accounts { get; set; }

    }

}
