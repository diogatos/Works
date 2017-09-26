using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class Account
    {
        public Customer customer;
        public Account(int id)
        {
            //customer = c;
            Balance = 50;
            ID = id;
        }
        public int ID { get; }

        public float Balance { get; set; }
    }
}
