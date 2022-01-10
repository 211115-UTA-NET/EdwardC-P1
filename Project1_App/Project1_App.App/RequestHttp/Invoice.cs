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
        private static GetStringInfo getStringInfo = null!;
 
        public static void SetUp(Uri myServer)
        {
            getStringInfo = new(myServer);
        }

        public static async Task<string> DisplayInvoicesByStoreId(string num)
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
            return getStringInfo.GetSummary(invoices);
        }

        public static async Task<List<string>> RetrieveInvoicesByStoreId(string num)
        {
            Dictionary<string, string> query = new() { ["storeId"] = num! };
            string requestUri = QueryHelpers.AddQueryString("/api/Invoices/StoreId", query);

            var response = await getStringInfo.SendRequestHttp(requestUri);
            var results = await response.Content.ReadFromJsonAsync<List<string>>();
            if (results == null)
            {
                throw new UnexpectedServerBehaviorException();
            }
            return results;
        }

        public static async Task<string> DisplayAllInvoices()
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
            return getStringInfo.GetSummary(invoices);
        }

        public static async Task<List<string>> RetrieveAllInvoices()
        {
            Dictionary<string, string> query = new();
            string requestUri = QueryHelpers.AddQueryString("/api/Invoices", query);

            var response = await getStringInfo.SendRequestHttp(requestUri);
            var results = await response.Content.ReadFromJsonAsync<List<string>>();
            if (results == null)
            {
                throw new UnexpectedServerBehaviorException();
            }
            return results;
        }

        public static async Task<string> DisplayInvoicesByCustomerId(string num)
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
            return getStringInfo.GetSummary(invoices);
        }

        public static async Task<List<string>> RetrieveInvoicesByCustomerId(string num)
        {
            Dictionary<string, string> query = new() { ["customerId"] = num! };
            string requestUri = QueryHelpers.AddQueryString("/api/Invoices/CustomerId", query);

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

