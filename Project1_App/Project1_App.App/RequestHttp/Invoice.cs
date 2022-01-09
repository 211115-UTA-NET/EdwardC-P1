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
    public class Invoice
    {
        public static readonly HttpClient httpClient = new();

        public Invoice(Uri serverUri)
        {
            httpClient.BaseAddress = serverUri;
        }

        public async Task<string> DisplayInvoicesByStoreId(string num)
        {
            List<string> invoices = new();
            try
            {
                invoices = await RetrieveInvoicesByStoreId(num);
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }
            return GetSummary(invoices);
        }

        public static async Task<List<string>> RetrieveInvoicesByStoreId(string num)
        {
            Dictionary<string, string> query = new() { ["storeId"] = num! };
            string requestUri = QueryHelpers.AddQueryString("/api/Invoices/StoreId", query);

            var Invoices = await SendRequestHttp(requestUri);
            return Invoices;
        }

        public async Task<string> DisplayAllInvoices()
        {
            List<string> invoices = new();
            try
            {
                invoices = await RetrieveAllInvoices();
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }
            return GetSummary(invoices);
        }

        public static async Task<List<string>> RetrieveAllInvoices()
        {
            Dictionary<string, string> query = new();
            string requestUri = QueryHelpers.AddQueryString("/api/Invoices", query);

            var Invoices = await SendRequestHttp(requestUri);
            return Invoices;
        }

        public async Task<string> DisplayInvoicesByCustomerId(string num)
        {
            List<string> invoices = new();
            try
            {
                invoices = await RetrieveInvoicesByCustomerId(num);
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server");
            }
            return GetSummary(invoices);
        }

        public static async Task<List<string>> RetrieveInvoicesByCustomerId(string num)
        {
            Dictionary<string, string> query = new() { ["customerId"] = num! };
            string requestUri = QueryHelpers.AddQueryString("/api/Invoices/CustomerId", query);

            var Invoices = await SendRequestHttp(requestUri);
            return Invoices;
        }

        public static string GetSummary(List<string> invoices)
        {
            var summary = new StringBuilder();
            summary.AppendLine("\n------------------\n");
            foreach (var invoice in invoices)
            {
                summary.AppendLine(invoice);
                summary.AppendLine();
            }
            summary.AppendLine("------------------");
            return summary.ToString();
        }

        public static async Task<List<string>> SendRequestHttp(string requestUri)
        {
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

            var Invoices = await response.Content.ReadFromJsonAsync<List<string>>();
            if (Invoices == null)
            {
                throw new UnexpectedServerBehaviorException();
            }
            return Invoices;
        }
    }
}

