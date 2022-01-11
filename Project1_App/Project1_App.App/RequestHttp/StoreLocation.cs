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

    public static class StoreLocation
    {
        public static async Task<string> DisplayStoreLocation()
        {
            List<string> storeLocations = new();
            try
            {
                storeLocations = await GetStoreLocations();
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }

            return GetStringInfo.GetSummary(storeLocations);
        }

        public static async Task<List<string>> GetStoreLocations()
        {
            Dictionary<string, string> query = new();
            string requestUri = QueryHelpers.AddQueryString("/api/StoreLocations", query);

            var response = await GetStringInfo.SendRequestHttp(requestUri);
            var results = await response.Content.ReadFromJsonAsync<List<string>>();
            if (results == null)
            {
                throw new UnexpectedServerBehaviorException();
            }
            return results;
        }

        public static async Task<string> DisplayStoreLocationById(int Id)
        {
            string? location = "";
            try
            {
                location = await GetStoreLocationById(Id);
            }
            catch(UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }
            return location;
        }

        public static async Task<string> GetStoreLocationById(int id)
        {
            Dictionary<string, string> query = new() { ["StoreId"] = id.ToString()};
            string requestUri = QueryHelpers.AddQueryString("/api/StoreLocations/Id", query);

            var response = await GetStringInfo.SendRequestHttp(requestUri);
            var results = await response.Content.ReadFromJsonAsync<string>();
            if (results == null)
            {
                throw new UnexpectedServerBehaviorException();
            }
            return results;
        }

        public static async Task<List<string>> DisplayItemListByStoreId(int id)
        {
            List<string> itemList = new() ;
            try
            {
                itemList = await GetItemsByStoreId(id);
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }
            return itemList;
        }

        public static async Task<List<string>> GetItemsByStoreId(int id)
        {
            Dictionary<string, string> query = new();
            string requestUri = QueryHelpers.AddQueryString("/api/StoreLocations", query);

            var response = await GetStringInfo.SendRequestHttp(requestUri);
            var results = await response.Content.ReadFromJsonAsync<List<string>>();
            if (results == null)
            {
                throw new UnexpectedServerBehaviorException();
            }
            return results;
        }
    }
}
