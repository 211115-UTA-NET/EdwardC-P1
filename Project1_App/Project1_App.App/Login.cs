using Microsoft.AspNetCore.WebUtilities;
using Project1_App.App.Exceptions;
using Project1_App.App.RequestHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Project1_App.App
{
    public class Login
    {
        public async Task LoginScreen(Program myProgram, Customer customer)
        {
            string? input;
            bool TryAgain = true;

            do
            {
                Console.WriteLine("Please choose a number from menu:\n1. New Customer\n2. Returned Customer\n3. Exit");
                input = Console.ReadLine();
                TryAgain = await GetUserInput(myProgram, customer, input);

            }
            while (TryAgain);
        }

        public async Task<bool> GetUserInput(Program myProgram, Customer customer, string? input)
        {
            int userInput;

            if (int.TryParse(input, out userInput))
            {
                switch (userInput)
                {
                    case 1:
                        myProgram.myMode = Program.Mode.NewCustomer;
                        return false;
                    case 2:
                        await GetUsernamePassword(myProgram, customer);
                        return false;
                    case 3:
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

        public async Task GetUsernamePassword(Program myProgram, Customer customer)
        {
            bool matching = false;
            bool IsManager = true;
            List<string> customerInfo = new();

            do
            {
                List<string?> inputs = new();
                Console.WriteLine("\nIf you want to cancel login. Then enter \"quit\" to close the program");
                Console.Write("Enter your username: ");
                inputs.Add(Console.ReadLine());
                if (inputs[0] == "quit")
                {
                    myProgram.myMode = Program.Mode.Exit;
                    break;
                }

                Console.Write("Enter your password: ");
                inputs.Add(Console.ReadLine());

                List<string> result = await customer.SearchLoginInfo(inputs!);
                if (result == null || result.Count == 0)
                {
                    matching = false;
                }
                else
                {
                    matching = true;
                    IsManager = Convert.ToBoolean(result![1]);
                    myProgram.CustomerId = Convert.ToInt32(result[0]);
                }
                if (matching)
                {
                    if (IsManager)
                    {
                        myProgram.myMode = Program.Mode.ManagerRequest;
                        break;
                    }
                    else
                    {
                        myProgram.myMode = Program.Mode.CustomerRequest;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Your username or password are invalid. Try again.");
                }
            }
            while (true);
        }
    }
}
