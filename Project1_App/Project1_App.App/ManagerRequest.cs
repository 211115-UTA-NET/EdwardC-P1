using Project1_App.App.RequestHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_App.App
{
    public class ManagerRequest
    {
        public async Task EnterManagerScreen(Program myProgram, Customer customer, StoreLocation storeLocation,
                                             StoreInventory storeInventory, Invoice invoice)
        {
            bool TryAgain = true;
            Console.WriteLine("Hello Manager, How we can help you?\n");

            do
            {
                Console.WriteLine("Manager Request:\nEnter the number followed by the menu:" +
                    "\n1. Check Store Inventory\n2. Check Invoice\n3. Search Customer's name\n4. Exit");
                string? input = Console.ReadLine();
                TryAgain = await GetUserInput(myProgram, storeLocation, storeInventory, invoice, customer, input);
            }
            while (TryAgain);
        }

        public async Task<bool> GetUserInput(Program myProgram, StoreLocation storeLocation, StoreInventory storeInventory,
                                             Invoice invoice, Customer customer, string? input)
        {
            int userInput = 0;
            if (int.TryParse(input, out userInput))
            {
                switch (userInput)
                {
                    case 1:
                        await CheckStoreInventory(storeLocation, storeInventory);
                        return true;
                    case 2:
                        await CheckStoreInvoices(storeLocation, invoice);
                        return true;
                    case 3:
                        await SearchCustomerName(customer);
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
        public async Task CheckStoreInventory(StoreLocation storeLocation, StoreInventory storeInventory)
        {
            string? input = "";
            bool TryAgain = false;

            do
            {
                Console.WriteLine("\nInventory\nChoose a store location");
                Console.Write(await storeLocation.GetStoreLocation());
                Console.WriteLine("4. All of the adove.");
                input = Console.ReadLine();

                TryAgain = await GetStoreInventoryById(storeInventory, input);
            }
            while (TryAgain);

        }

        public static async Task<bool> GetStoreInventoryById(StoreInventory storeInventory, string? input)
        {
            int userInput = 0;

            if (int.TryParse(input, out userInput))
            {
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine(await storeInventory.DisplayStoreInventorysById("1"));
                        return false;
                    case 2:
                        Console.WriteLine(await storeInventory.DisplayStoreInventorysById("2"));
                        return false;
                    case 3:
                        Console.WriteLine(await storeInventory.DisplayStoreInventorysById("3"));
                        return false;
                    case 4:
                        Console.WriteLine(await storeInventory.DisplayAllStoreInventorys());
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
        public static async Task CheckStoreInvoices(StoreLocation storeLocation, Invoice invoice)
        {
            string? input = "";
            bool TryAgain = false;

            do
            {
                Console.WriteLine("\nInvoices:\nChoose a store location");
                Console.Write(await storeLocation.GetStoreLocation());
                Console.WriteLine("4. All of the adove.");
                input = Console.ReadLine();

                TryAgain = await GetStoreInvoices(invoice, input);
            }
            while (TryAgain);
        }

        public static async Task<bool> GetStoreInvoices(Invoice invoice, string? input)
        {
            int userInput = 0;
            if (int.TryParse(input, out userInput))
            {
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine(await invoice.DisplayInvoicesByStoreId("1"));
                        return false;
                    case 2:
                        Console.WriteLine(await invoice.DisplayInvoicesByStoreId("2"));
                        return false;
                    case 3:
                        Console.WriteLine(await invoice.DisplayInvoicesByStoreId("3"));
                        return false;
                    case 4:
                        Console.WriteLine(await invoice.DisplayAllInvoices());
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
        public static async Task SearchCustomerName(Customer customer)
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
                TryAgain = await FindCustomer(customer, input, name);
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
        public static async Task<bool> FindCustomer(Customer customer, string? input, string? name)
        {
            int userInput = 0;
            if (int.TryParse(input, out userInput))
            {
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine(await customer.FindCustomerName(input, name));
                        return false;
                    case 2:
                        Console.WriteLine(await customer.FindCustomerName(input, name));
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

