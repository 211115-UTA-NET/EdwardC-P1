using Project1_App.App.Dtos;
using Project1_App.App.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project1_App.App.RequestHttp
{
    public static class ModifyInformation
    {
        public static readonly HttpClient httpClient = new();
        public static void getStringInfo(Uri myServer)
        {
            httpClient.BaseAddress = myServer;
        }

        public static async Task SendRequestHttpPost(NewCustomerInfo Info)
        {
            HttpRequestMessage request = new(HttpMethod.Post, "/api/Customers/Add");
            request.Content = new StringContent(JsonSerializer.Serialize(Info), 
                Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpResponseMessage response;
            try
            {
                response = await httpClient.PostAsync("/api/Customers", request.Content);
            }
            catch (HttpRequestException ex)
            {
                throw new UnexpectedServerBehaviorException("network error", ex);
            }
            Console.WriteLine();
        }
    }
}
