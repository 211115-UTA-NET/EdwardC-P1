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
    public static class StoreInventory
    {
        private static GetStringInfo getStringInfo = null!;

        public static void SetUp(Uri myServer)
        {
            getStringInfo = new(myServer);
        }

        public static async Task<string> DisplayStoreInventorysById(string num)
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

            return getStringInfo.GetSummary(storeInventorys);
        }

        public static async Task<List<string>> RetrieveStoreInventoryById(string? num)
        {
            Dictionary<string, string> query = new() { ["Id"] = num! };
            string requestUri = QueryHelpers.AddQueryString("/api/StoreInventorys/Id", query);

            var response = await getStringInfo.SendRequestHttp(requestUri);
            var results = await response.Content.ReadFromJsonAsync<List<string>>();
            if (results == null)
            {
                throw new UnexpectedServerBehaviorException();
            }
            return results;
        }

        public static async Task<string> DisplayAllStoreInventorys()
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
            return getStringInfo.GetSummary(storeInventorys);
        }

        public static async Task<List<string>> RetrieveAllStoreInventory()
        {
            Dictionary<string, string> query = new();
            string requestUri = QueryHelpers.AddQueryString("/api/StoreInventorys", query);

            var response = await getStringInfo.SendRequestHttp(requestUri);
            var results = await response.Content.ReadFromJsonAsync<List<string>>();
            if (results == null)
            {
                throw new UnexpectedServerBehaviorException();
            }
            return results;
        }
    }
}

