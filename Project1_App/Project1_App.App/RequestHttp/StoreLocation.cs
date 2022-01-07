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

        public StoreLocation(Uri serverUri)
        {
            httpClient.BaseAddress = serverUri;
        }
        public async Task<string> GetStoreLocation()
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

            var summary = new StringBuilder();
            for (int i = 0; i < storeLocations.Count(); i++)
            {
                summary.AppendLine($"{storeLocations[i]}");
            }

            return summary.ToString();
        }

        public async Task<List<string>> GetStoreLocations()
        {
            Dictionary<string, string> query = new();
            string requestUri = QueryHelpers.AddQueryString("/api/StoreLocations", query);

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

            var storeLocations = await response.Content.ReadFromJsonAsync<List<string>>();
            if (storeLocations == null)
            {
                throw new UnexpectedServerBehaviorException();
            }

            return storeLocations;
        }
    }
}
