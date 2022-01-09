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
        private static Uri? ServerUri;
        private static GetStringInfo getStringInfo = null!;
        public Invoice(Uri serverUri)
        {
            //httpClient.BaseAddress = serverUri;
            ServerUri = serverUri;
        }
        public static void SetUp()
        {
            getStringInfo = new(ServerUri!);
        }

        public async Task<string> DisplayInvoicesByStoreId(string num)
        {
            SetUp();
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

        public async Task<List<string>> RetrieveInvoicesByStoreId(string num)
        {
            Dictionary<string, string> query = new() { ["storeId"] = num! };
            string requestUri = QueryHelpers.AddQueryString("/api/Invoices/StoreId", query);

            var Invoices = await getStringInfo.SendRequestHttp(requestUri);
            return Invoices;
        }

        public async Task<string> DisplayAllInvoices()
        {
            SetUp();
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

        public async Task<List<string>> RetrieveAllInvoices()
        {
            Dictionary<string, string> query = new();
            string requestUri = QueryHelpers.AddQueryString("/api/Invoices", query);

            var Invoices = await getStringInfo.SendRequestHttp(requestUri);
            return Invoices;
        }

        public async Task<string> DisplayInvoicesByCustomerId(string num)
        {
            SetUp();
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

        public async Task<List<string>> RetrieveInvoicesByCustomerId(string num)
        {
            Dictionary<string, string> query = new() { ["customerId"] = num! };
            string requestUri = QueryHelpers.AddQueryString("/api/Invoices/CustomerId", query);

            var Invoices = await getStringInfo.SendRequestHttp(requestUri);
            return Invoices;
        }
    }
}

