using Project1_App.App.RequestHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_App.App
{
    public class CustomerRequest
    {
        public async Task EnterCustomerScreen(Program myProgram, StoreLocation storeLocation, Invoice invoice, ItemDetailsInfo itemDetailsInfo)
        {
            bool TryAgain = false;

            await ChooseStore(myProgram, storeLocation);

            do
            {
                Console.WriteLine("\nPlease choose a number from menu:\n1. Check your order hisoty\n2. See item's information" +
                                  "\n3. Check store history\n4. Ready to order items\n5. Log out");
                string? input = Console.ReadLine();
                TryAgain = await getUserInput(myProgram, invoice, itemDetailsInfo, input);
            }
            while (TryAgain);
        }

        public static async Task ChooseStore(Program myProgram, StoreLocation storeLocation)
        {
            int num = 0;
            Console.WriteLine("Pick a location followed: ");

            while (true)
            {
                Console.Write(await storeLocation.GetStoreLocation());
                if (int.TryParse(Console.ReadLine(), out num))
                {
                    if (0 < num && num < 4)
                    {
                        myProgram.StoreId = num;
                        break;
                    }
                    else
                        Console.WriteLine("Your input is invalid: Value is not matching any number from menu. Please try again");
                }
                else
                {
                    Console.WriteLine("Your input is invalid: Value is not number");
                }
            }

        }
        public static async Task<bool> getUserInput(Program myProgram, Invoice invoice, ItemDetailsInfo itemDetailsInfo, string? input)
        {
            int userInput = 0;
            if (int.TryParse(input, out userInput))
            {
                switch (userInput)
                {
                    case 1:
                        Console.Write("\nYour Invoices");
                        Console.Write(await invoice.DisplayInvoicesByCustomerId(myProgram.CustomerId.ToString()));
                        break;
                    case 2:
                        Console.Write("\nItem's Detail:");
                        Console.Write(await itemDetailsInfo.DisplayItems());
                        break;
                    case 3:
                        Console.Write("\nStore's Invoices:");
                        Console.Write(await invoice.DisplayInvoicesByStoreId(myProgram.StoreId.ToString()));
                        break;
                    case 4:
                        myProgram.myMode = Program.Mode.SetOrder;
                        return false;
                    case 5:
                        myProgram.myMode = Program.Mode.Login;
                        Console.WriteLine();
                        return false;
                    default:
                        Console.WriteLine("Your input is invalid: Value is not matching any number from menu. Please try again");
                        break;
                }
                return true;
            }
            else
            {
                Console.WriteLine("Your input is invalid: Value is not number. Please try again.");
                return true;
            }
        }
    }
}

