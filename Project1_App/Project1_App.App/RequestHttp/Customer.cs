using Microsoft.AspNetCore.WebUtilities;
using Project1_App.App.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Project1_App.App.RequestHttp
{
    public static class Customer
    {
        public static async Task<List<string>> SearchLoginInfo(List<string> inputs)
        {
            List<string> loginInfo = new();
            try
            {
                loginInfo = await FoundLoginInfo(inputs);
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }

            return loginInfo;
        }

        public static async Task<List<string>> FoundLoginInfo(List<string> inputs)
        {
            Dictionary<string, string> query = new() { ["Username"] = inputs[0]!.ToString(), ["Password"] = inputs[1]!.ToString() };
            string requestUri = QueryHelpers.AddQueryString("/api/Logins", query);

            var response = await GetStringInfo.SendRequestHttp(requestUri);
            var results = await response.Content.ReadFromJsonAsync<List<string>>();
            if (results == null)
            {
                throw new UnexpectedServerBehaviorException();
            }
            return results;
        }

        public static async Task<string> FindCustomerName(string? input, string? name)
        {
            bool isFound = false;
            try
            {
                isFound = await SearchName(name);
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }

            if (isFound)
            {
                if (input == "1")
                    return $"Customer's First name: {name} is found!";
                else
                    return $"Customer's Last name: {name} is Found!";
            }
            else
            {
                if (input == "2")
                    return $"Customer's First name: {name} is not found!";
                else
                    return $"Customer's Last name: {name} is not found!";
            }
        }

        public static async Task<bool> SearchName(string? name)
        {
            Dictionary<string, string> query = new() { ["Name"] = name! };
            string requestUri = QueryHelpers.AddQueryString("/api/Customers/Name", query);
            
            var response = await GetStringInfo.SendRequestHttp(requestUri);
            var isFound = await response.Content.ReadFromJsonAsync<bool>();
            return isFound;
        }

        public static async Task AddCustomer(List<string> inputs)
        {
            Dictionary<string, string> query = new() { ["FirstName"] = inputs[0], ["LastName"] = inputs[1], ["Phone"] = inputs[2], 
                                                       ["Address"] = inputs[3], ["Username"] = inputs[4], ["Password"] = inputs[5] };
            string requestUri = QueryHelpers.AddQueryString("/api/Customers/Add", query);

            await ModifyInformation.SendRequestHttpPost(requestUri);
        }
    }
}

