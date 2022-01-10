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
    public class ItemDetailsInfo
    {
        private static GetStringInfo getStringInfo = null!;

        public static void SetUp(Uri myServer)
        {
            getStringInfo = new(myServer);
        }

        public static async Task<string> DisplayItems()
        {
            List<string> itemDetails = new();
            try
            {
                itemDetails = await GetItems();
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }
            return getStringInfo.GetSummary(itemDetails);
            
        }

        public static async Task<List<string>> GetItems()
        {
            Dictionary<string, string> query = new();
            string requestUri = QueryHelpers.AddQueryString("/api/ItemDetails", query);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

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