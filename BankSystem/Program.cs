using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class Program
    {
        //For customer IDs
        static int customerCounter = 1;
        static int accountCounter = 1;
        static Bank theBank = new Bank();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Banking System.\n---------------\n");
            InitializeBankSystem();
        }
        //VER RETURNS
        public static void InitializeBankSystem()
        {

            Console.WriteLine("Choose an option by typing its number please.\n1. Create Customer.\n2. Find Customer.\n3. Delete Customer.\n4. Edit Customer.\n5. Transfer money.\n6. Exit");
            //talvez adicionar opcao para um cliente ter mais de que uma conta.
            int option = Int32.Parse(Console.ReadLine());
            switch (option)
            {
                //go to create customer function;
                case 1: AddCustomer(customerCounter); break; 
                //Go to find customer(s);
                case 2:
                    Console.Clear();
                    if (theBank.AllCustomers.Count == 0)
                    {
                        Console.WriteLine("\nNo customers on data base yet! Returning to main menu.\n");
                        InitializeBankSystem();
                    }
                    Console.WriteLine("1. Show ALL customers\n2. Find specific customer");
                    option = Int32.Parse(Console.ReadLine());
                    if (option == 1) ShowAllCustomers();
                    else if (option == 2) {
                        Console.WriteLine("Enter the customer ID.");
                        int id = Int32.Parse(Console.ReadLine());
                        FindCustomer(id);
                    }
                    break;
                //Go to delete customer;
                case 3:
                    Console.Clear();
                    if (theBank.AllCustomers.Count == 0)
                    {
                        Console.WriteLine("\nNo customers on data base yet! Returning to main menu.\n");
                        InitializeBankSystem();
                    }
                    DeleteCustomer();
                    break;
                //Go to edit customer;
                case 4:
                    Console.Clear();
                    EditCustomer();
                    break; 
                //Go to transfer money;
                case 5:
                    Console.Clear();
                    if (theBank.AllCustomers.Count < 2)
                    {
                        Console.WriteLine("\nNot enough customers on data base to make a transfer! Returning to main menu.\n");
                        InitializeBankSystem();
                    }
                    TransferMoney();
                    break;
                //Exit application
                case 6: System.Environment.Exit(0); break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice.");
                    InitializeBankSystem();
                    break;
            }
        }

        //Creates customer and his account
        public static void AddCustomer(int id)
        {
            Console.Clear();
            try
            {
                Customer newCustomer = new Customer(id);
                Console.WriteLine("Need account holder first and last name:");
                string name = Console.ReadLine();
                string[] splitName = name.Split(' ');
                if (splitName.Length == 2)
                {
                    newCustomer.FirstName = splitName[0].ToUpper();
                    newCustomer.LastName = splitName[1].ToUpper();
                    //Checks if customer already exists, if it does creates new account or not, depending on choice
                    if (theBank.AllCustomers.Count > 1)
                    {
                        bool customerFound = false;
                        foreach (Customer c in theBank.AllCustomers)
                        {
                            if ((c.FirstName == newCustomer.FirstName) && (c.LastName == newCustomer.LastName))
                            {
                                customerFound = true;
                                Console.WriteLine("Customer already exists, create new account for this customer? Y/N");
                                string option = Console.ReadLine();
                                if (option.ToUpper() == "Y")
                                {
                                    c.Accounts.Add(new Account(accountCounter));
                                    choicePannel(1);
                                }
                                else InitializeBankSystem();
                                break;
                            }
                        }
                        if (!customerFound)
                        {
                            newCustomer.Accounts.Add(new Account(accountCounter));
                            theBank.AllCustomers.Add(newCustomer);
                            customerCounter++;
                        }
                    }
                    else
                    {
                        newCustomer.Accounts.Add(new Account(accountCounter));
                        theBank.AllCustomers.Add(newCustomer);
                        customerCounter++;
                    }
                    accountCounter++;
                    choicePannel(1);
                }
                else AddCustomer(id);
            }
            catch
            {
                throw new Exception("ERROR CREATING CUSTOMER");

            }
        }
        //SOMETHING CHANGED
        //Show ALL customers
        public static void ShowAllCustomers()
        {
            Console.Clear();
            Console.WriteLine("----ALL CUSTOMERS----");
            foreach(Customer c in theBank.AllCustomers)
            {
                Console.WriteLine("ID: "+c.ID + "   \tName: " + c.FirstName + " " + c.LastName+ "   \tNumber of Accounts: "+c.Accounts.Count);
            }
            Console.WriteLine("-----------------\n");
            choicePannel(2);
        }


        //Show specific customer
        public static void FindCustomer(int n)
        {
            foreach (Customer c in theBank.AllCustomers)
            {
                if(n == c.ID)
                {
                    Console.Clear();
                    Console.WriteLine("ID: " + c.ID + "   Name: " + c.FirstName + " " + c.LastName + "   Number of Accounts: " + c.Accounts.Count);
                    Console.WriteLine("\n-----Associated accounts-----");
                    foreach(Account a in c.Accounts)
                    {
                        Console.WriteLine("Account ID: " + a.ID + "  Balance: " + a.Balance+"\n");
                    }
                    choicePannel(2);
                }
            }
            Console.Clear();
            Console.WriteLine("Customer not found, either doesn't exist or wrong ID inserted.\nReturning to main menu.\n");
            InitializeBankSystem();
        }
        
        //Deletes chosen customer and linked accounts
        public static void DeleteCustomer()
        {
            Console.WriteLine("Enter the ID of the account you wish to delete.");
            int id = Int32.Parse(Console.ReadLine());
            foreach(Customer c in theBank.AllCustomers)
            {
                if (id == c.ID)
                {
                    Console.WriteLine("Are you sure you want to remove this account?  Y/N");
                    string choice = Console.ReadLine();
                    if (choice.ToUpper() == "Y")
                    {
                        c.Accounts.Clear();
                        theBank.AllCustomers.Remove(c);
                        Console.WriteLine("Customer deleted. Returning to main menu.\n"); InitializeBankSystem();
                    }
                    else { Console.WriteLine("Returning to main menu.\n"); InitializeBankSystem(); }
                }
            }
            Console.WriteLine("Customer not found. Returning to main menu.\n");
            InitializeBankSystem();
        }

        //Edit chosen customer
        public static void EditCustomer()
        {
            Console.WriteLine("Enter the ID of the account you wish to edit.");
            int id = Int32.Parse(Console.ReadLine());
            foreach(Customer c in theBank.AllCustomers)
            {
                if(c.ID == id)
                {
                    EditingChoices(c);
                    Console.WriteLine("Changed customer ID " + c.ID + " information successfully.\n Returning to main menu.");
                    InitializeBankSystem();
                }
            }
            Console.WriteLine("No such customer exists. Returning to main menu.\n");
            InitializeBankSystem();
        }

        public static void EditingChoices(Customer c)
        {
            Console.WriteLine("Choose what to edit:\n1. First Name.\n2. Last Name");
            int option = Int32.Parse(Console.ReadLine());
            if (option == 1)
            {
                Console.WriteLine("Enter the new first name.");
                c.FirstName = Console.ReadLine();
            }
            else if (option == 2)
            {
                Console.WriteLine("Enter the new last name.");
                c.LastName = Console.ReadLine();
            }
            else { Console.WriteLine("Invalid choice. Choose a valid option.\n"); EditingChoices(c); }
        }

        //Deposits money on chosen account    
        public static void DepositMoney()
        {
            Console.WriteLine("Enter the ID of the account you wish to deposit money into.");
            int accId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter the ammount of money you want to deposit.");
            int money = Int32.Parse(Console.ReadLine());
            foreach(Customer c in theBank.AllCustomers)
            {
                foreach(Account a in c.Accounts)
                { 
                    if(accId == a.ID)
                    {
                        Console.WriteLine("You are depositing into " + c.FirstName + " " + c.LastName + " account with the " + a.ID + " ID, is this correct?");
                        string choice = Console.ReadLine();
                        if(choice.ToUpper() == "Y")
                            a.Balance += money;
                        else { Console.WriteLine("Going to main menu."); InitializeBankSystem(); }
                    }
                }
            }
            Console.WriteLine("Customer not found. Returning to main menu.\n");
            InitializeBankSystem();
        }
        //Transfers money from Id1 to Id2       MONEY NEEDS TO GO TO ACCOUNTS
        public static void TransferMoney()
        {
            Console.WriteLine("Enter the ID of the account you wish to transfer money FROM.");
            int accId1 = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter the ammount of money you want to transfer.");
            int money = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter the ID of the account you wish to transfer money TO.");
            int accId2 = Int32.Parse(Console.ReadLine());
            foreach(Customer c1 in theBank.AllCustomers)
            {
                foreach (Account a1 in c1.Accounts)
                {
                    if (a1.ID == accId1)
                    {
                        //If transfering gets first account below 0 , it won't let it transfer
                        if ((a1.Balance - money) < 0)
                        {
                            Console.WriteLine("\nThis account is too short on money to make this transfer.\n1. Enter new amount.\n2. Return to main menu.\n");
                            choicePannel(3);
                        }
                        else
                        {
                            foreach (Customer c2 in theBank.AllCustomers)
                            {
                                foreach (Account a2 in c2.Accounts)
                                {
                                    if (a2.ID == accId2)
                                    {
                                        a1.Balance -=  money;
                                        a2.Balance +=  money;
                                        Console.WriteLine("Ammount transfered.\n");
                                        choicePannel(2);
                                    }
                                }
                            }
                            Console.WriteLine("Couldn't find account with ID " + accId2 + ". Returning to main menu.\n");
                            InitializeBankSystem();
                        }
                    }
                }
            }
            Console.WriteLine("Couldn't find account with ID " + accId1 + ". Returning to main menu.\n");
            InitializeBankSystem();
        }

        //Branching of choices, to repeat/do another action, or go back to main menu
        public static void choicePannel(int n)
        {   
            //Options after create customer.  Jumps to create new customer or main menu
            if (n == 1)
            {
                Console.WriteLine("\nWould you like to:\n1. Create another customer.\n2. Return to main menu.");
                int option = Int32.Parse(Console.ReadLine());
                Console.Clear();
                if (option == 1) AddCustomer(customerCounter);
                else if (option == 2) InitializeBankSystem();
                else WrongChoice(1);
            }
            //Options after Find one or Show All Customers.  Jumps to Deposit, Transfer or Main menu
            else if(n == 2)
            {
                Console.WriteLine("Would you like to:\n1. Find specific customer.\n2. Deposit money.\n3. Transfer money.\n4. Return to main menu.");
                int option = Int32.Parse(Console.ReadLine());
                if (option == 1) {
                    Console.WriteLine("Enter the customer ID.");
                    int id = Int32.Parse(Console.ReadLine());
                    FindCustomer(id);
                }
                else if (option == 2) DepositMoney();
                else if (option == 3) TransferMoney();
                else if (option == 4) InitializeBankSystem();
                else WrongChoice(2);
            } //Options after Transfer money
            else if (n == 3)
            {
                int option = Int32.Parse(Console.ReadLine());
                if (option == 1) TransferMoney();
                else if (option == 2) InitializeBankSystem();
                else WrongChoice(3);
            } 
        }

        //Just for wrong choices while branching, keeps out the clutter from code
        public static void WrongChoice(int n)
        {
            Console.WriteLine("Wrong Choice! Returning to options.");
            Console.Clear();
            choicePannel(n);
        }
    }
}
