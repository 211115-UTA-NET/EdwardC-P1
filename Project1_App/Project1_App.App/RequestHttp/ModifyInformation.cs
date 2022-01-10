using Project1_App.App.Dtos;
using Project1_App.App.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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
            HttpRequestMessage request = new(HttpMethod.Post, "/api/Customers");
            request.Content = new StringContent(JsonSerializer.Serialize(Info), 
                Encoding.UTF8, MediaTypeNames.Application.Json);

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
            //return response;
        }
    }
}
