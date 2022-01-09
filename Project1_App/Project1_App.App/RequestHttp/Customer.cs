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
    public class Customer
    {
        public static readonly HttpClient httpClient = new();

        public Customer(Uri serverUri)
        {
            httpClient.BaseAddress = serverUri;
        }

        public async Task<List<string>> SearchLoginInfo(List<string> inputs)
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

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            try
            {
                response = await httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new UnexpectedServerBehaviorException("network error", ex);
            }

            response.EnsureSuccessStatusCode();
            if (response.Content.Headers.ContentType?.MediaType != MediaTypeNames.Application.Json)
            {
                throw new UnexpectedServerBehaviorException();
            }

            var getLogin = await response.Content.ReadFromJsonAsync<List<string>>();
            if (getLogin == null)
            {
                throw new UnexpectedServerBehaviorException();
            }

            return getLogin;

        }

        public async Task<string> FindCustomerName(string? input, string? name)
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

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            try
            {
                response = await httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new UnexpectedServerBehaviorException("network error", ex);
            }

            response.EnsureSuccessStatusCode();
            if (response.Content.Headers.ContentType?.MediaType != MediaTypeNames.Application.Json)
            {
                throw new UnexpectedServerBehaviorException();
            }

            var isFound = await response.Content.ReadFromJsonAsync<bool>();

            return isFound;
        }
    }
}

