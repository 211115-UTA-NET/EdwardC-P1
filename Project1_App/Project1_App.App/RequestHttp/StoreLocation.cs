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

    public class StoreLocation
    {
        public static readonly HttpClient httpClient = new();
        private static Uri? ServerUri;
        private static GetStringInfo getStringInfo = null!;

        public StoreLocation(Uri serverUri)
        {
            ServerUri = serverUri;
        }
        public static void SetUp()
        {
            getStringInfo = new(ServerUri!);
        }
        public async Task<string> GetStoreLocation()
        {
            //SetUp();
            List<string> storeLocations = new();
            try
            {
                storeLocations = await GetStoreLocations();
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }

            return getStringInfo.GetSummary(storeLocations);

            //var summary = new StringBuilder();
            //for (int i = 0; i < storeLocations.Count; i++)
            //{
            //    summary.AppendLine($"{storeLocations[i]}");
            //}

            //return summary.ToString();
        }

        public static async Task<List<string>> GetStoreLocations()
        {
            Dictionary<string, string> query = new();
            string requestUri = QueryHelpers.AddQueryString("/api/StoreLocations", query);

            var storeLocations = await getStringInfo.SendRequestHttp(requestUri);
            return storeLocations;
        }
    }
}
