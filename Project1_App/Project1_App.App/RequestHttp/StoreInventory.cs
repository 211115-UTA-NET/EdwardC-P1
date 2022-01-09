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
    public class StoreInventory
    {
        public static readonly HttpClient httpClient = new();

        public StoreInventory(Uri serverUri)
        {
            httpClient.BaseAddress = serverUri;
        }

        public async Task<string> DisplayStoreInventorysById(string num)
        {
            List<string> storeInventorys = new();
            try
            {
                storeInventorys = await RetrieveStoreInventoryById(num);
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }

            var summary = new StringBuilder();
            summary.AppendLine("\n------------------\n");
            foreach (var storeInventory in storeInventorys)
            {
                summary.AppendLine(storeInventory);
                summary.AppendLine();
            }
            summary.AppendLine("------------------");

            return summary.ToString();
        }

        public static async Task<List<string>> RetrieveStoreInventoryById(string? num)
        {
            Dictionary<string, string> query = new() { ["Id"] = num! };
            string requestUri = QueryHelpers.AddQueryString("/api/StoreInventorys/Id", query);

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

            var storeInventorys = await response.Content.ReadFromJsonAsync<List<string>>();
            if (storeInventorys == null)
            {
                throw new UnexpectedServerBehaviorException();
            }

            return storeInventorys;
        }

        public async Task<string> DisplayAllStoreInventorys()
        {
            List<string> storeInventorys = new();
            try
            {
                storeInventorys = await RetrieveAllStoreInventory();
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }

            var summary = new StringBuilder();
            summary.AppendLine("\n------------------\n");
            foreach (var storeInventory in storeInventorys)
            {
                summary.AppendLine(storeInventory);
                summary.AppendLine();
            }
            summary.AppendLine("------------------");

            return summary.ToString();
        }

        public static async Task<List<string>> RetrieveAllStoreInventory()
        {
            Dictionary<string, string> query = new();
            string requestUri = QueryHelpers.AddQueryString("/api/StoreInventorys", query);

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

            var storeInventorys = await response.Content.ReadFromJsonAsync<List<string>>();
            if (storeInventorys == null)
            {
                throw new UnexpectedServerBehaviorException();
            }

            return storeInventorys;
        }

    }
}

