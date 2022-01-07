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
        public static readonly HttpClient httpClient = new();

        public ItemDetailsInfo(Uri serverUri)
        {
            httpClient.BaseAddress = serverUri;
        }

        public async Task<string> DisplayItems()
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

            var summary = new StringBuilder();
            summary.AppendLine("\n------------------\n");
            foreach (var detail in itemDetails)
            {
                summary.AppendLine(detail);
                summary.AppendLine();
            }
            summary.AppendLine("------------------");
            return summary.ToString();
        }

        public async Task<List<string>> GetItems()
        {
            Dictionary<string, string> query = new();
            string requestUri = QueryHelpers.AddQueryString("/api/ItemDetails", query);

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

            var getItemDetail = await response.Content.ReadFromJsonAsync<List<string>>();
            if (getItemDetail == null)
            {
                throw new UnexpectedServerBehaviorException();
            }

            return getItemDetail;
        }
    }
}
