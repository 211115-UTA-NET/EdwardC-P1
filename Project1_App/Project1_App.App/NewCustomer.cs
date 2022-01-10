using Project1_App.App.RequestHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_App.App
{
    public static class NewCustomer
    {
        public static async Task EnterNewCustomerScreen(Program program)
        {
            List<string> inputs = new();
            string? input;

            Console.WriteLine("Please enter the information:");
            for(int i = 0; i < 6; i++)
            {
                Console.WriteLine(getString(i));
                input = getUserInput();
                if (i == 4 && input?.Length >= 11)
                {
                    Console.WriteLine("You enter more than 10 digit. Try again");
                    input = null;
                    i--;
                }
                if (input != null || input!.Length > 0)
                    inputs.Add(input);
            }

            await Customer.AddCustomer(inputs);
            program.myMode = Program.Mode.Login;
        }

        public static string getString(int num)
        {
            string? getRequest = "";
            switch (num)
            {
                case 0:
                    getRequest = "First Name: ";
                    break;
                case 1:
                    getRequest = "Last Name: ";
                    break;
                case 2:
                    getRequest = "Phone Number: ";
                    break;
                case 3:
                    getRequest = "Home Address: ";
                    break;

                case 4:
                    getRequest = "Username: ";
                    break;
                case 5:
                    getRequest = "Password: ";
                    break;
            }

            return getRequest;
        }

        public static string getUserInput()
        {
            string? input = Console.ReadLine();
            return input!;
        }

    }
}
