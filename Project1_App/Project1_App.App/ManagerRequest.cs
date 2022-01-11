using Project1_App.App.RequestHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_App.App
{
    public static class ManagerRequest
    {
        public static async Task EnterManagerScreen(Program myProgram)
        {
            bool TryAgain = true;
            Console.WriteLine("Hello Manager, How we can help you?\n");

            do
            {
                Console.WriteLine("Manager Request:\nEnter the number followed by the menu:" +
                    "\n1. Check Store Inventory\n2. Check Invoice\n3. Search Customer's name\n4. Exit");
                string? input = Console.ReadLine();
                TryAgain = await GetUserInput(myProgram, input);
            }
            while (TryAgain);
        }

        public static async Task<bool> GetUserInput(Program myProgram, string? input)
        {
            int userInput = 0;
            if (int.TryParse(input, out userInput))
            {
                switch (userInput)
                {
                    case 1:
                        await CheckStoreInventory();
                        return true;
                    case 2:
                        await CheckStoreInvoices();
                        return true;
                    case 3:
                        await SearchCustomerName();
                        return true;
                    case 4:
                        myProgram.myMode = Program.Mode.Exit;
                        return false;
                    default:
                        Console.WriteLine("Your input is invalid: Value don't match any menu. Please try again");
                        return true;
                }
            }
            else
            {
                Console.WriteLine("Your input is invalid: Value is not number. Please try again.");
                return true;
            }
        }

        // Inventory
        public static async Task CheckStoreInventory()
        {
            string? input = "";
            bool TryAgain = false;

            do
            {
                Console.WriteLine("\nInventory\nChoose a store location");
                Console.Write(await StoreLocation.DisplayStoreLocation());
                Console.WriteLine("4. All of the adove.");
                input = Console.ReadLine();

                TryAgain = await GetStoreInventoryById(input);
            }
            while (TryAgain);

        }

        public static async Task<bool> GetStoreInventoryById(string? input)
        {
            int userInput = 0;

            if (int.TryParse(input, out userInput))
            {
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine(await StoreInventory.DisplayStoreInventorysById("1"));
                        return false;
                    case 2:
                        Console.WriteLine(await StoreInventory.DisplayStoreInventorysById("2"));
                        return false;
                    case 3:
                        Console.WriteLine(await StoreInventory.DisplayStoreInventorysById("3"));
                        return false;
                    case 4:
                        Console.WriteLine(await StoreInventory.DisplayAllStoreInventorys());
                        return false;
                    default:
                        Console.WriteLine("Your input is invalid: Value is not matching on any menu. Please try again");
                        return true;
                }
            }
            else
            {
                Console.WriteLine("Your input is invalid: Value is not number. Please try again");
                return true;
            }
        }

        // Invoices
        public static async Task CheckStoreInvoices()
        {
            string? input = "";
            bool TryAgain = false;

            do
            {
                Console.WriteLine("\nInvoices:\nChoose a store location");
                Console.Write(await StoreLocation.DisplayStoreLocation());
                Console.WriteLine("4. All of the adove.");
                input = Console.ReadLine();

                TryAgain = await GetStoreInvoices(input);
            }
            while (TryAgain);
        }

        public static async Task<bool> GetStoreInvoices(string? input)
        {
            int userInput = 0;
            if (int.TryParse(input, out userInput))
            {
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine(await Invoice.DisplayInvoicesByStoreId("1"));
                        return false;
                    case 2:
                        Console.WriteLine(await Invoice.DisplayInvoicesByStoreId("2"));
                        return false;
                    case 3:
                        Console.WriteLine(await Invoice.DisplayInvoicesByStoreId("3"));
                        return false;
                    case 4:
                        Console.WriteLine(await Invoice.DisplayAllInvoices());
                        return false;
                    default:
                        Console.WriteLine("Your input is invalid: Value is not matching any number from menu. Please try again");
                        return false;
                }
            }
            else
            {
                Console.WriteLine("Your input is invalid: Value is not number. Please try again.");
                return true;
            }
        }

        // Customers
        public static async Task SearchCustomerName()
        {
            int num = 0;
            string? input = "";
            string? name = "";
            bool TryAgain = false;

            do
            {
                Console.WriteLine("\nSearch Customer:\nEnter the number followed by the menu: " +
                                  "\n1. First Name\n2. Last Name");
                input = Console.ReadLine();
                if (int.TryParse(input, out num))
                    name = GetName();
                TryAgain = await FindCustomer(input, name);
            }
            while (TryAgain);
            Console.WriteLine("\n------------------\n");
        }
        public static string GetName()
        {
            string? input = "";
            Console.Write("\nEnter the customer's name: ");
            input = Console.ReadLine();
            return input!;
        }
        public static async Task<bool> FindCustomer(string? input, string? name)
        {
            int userInput = 0;
            if (int.TryParse(input, out userInput))
            {
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine(await Customer.FindCustomerName(input, name));
                        return false;
                    case 2:
                        Console.WriteLine(await Customer.FindCustomerName(input, name));
                        return false;
                    default:
                        Console.WriteLine("Your input is invalid: Value is not matching any number from menu. Please try again");
                        return true;
                }
            }
            else
            {
                Console.WriteLine("Your input is invalid: Value is not number. Please try again.");
                return true;
            }
        }
    }
}

