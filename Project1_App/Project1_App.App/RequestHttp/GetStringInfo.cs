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
    public class GetStringInfo
    {
        public static readonly HttpClient httpClient = new();
        public GetStringInfo(Uri myServer)
        {
            httpClient.BaseAddress = myServer;
        }

        public  static string GetSummary(List<string> results)
        {
            var summary = new StringBuilder();
            summary.AppendLine("\n------------------\n");
            foreach (var result in results)
            {
                summary.AppendLine(result);
                summary.AppendLine();
            }
            summary.AppendLine("------------------");
            return summary.ToString();
        }
        public static async Task<HttpResponseMessage> SendRequestHttp(string requestUri)
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
            return response;
        }
    }
}
