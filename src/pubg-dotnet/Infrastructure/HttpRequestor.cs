﻿using Pubg.Net.Exceptions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Pubg.Net.Infrastructure
{
    internal static class HttpRequestor
    {
        internal static HttpClient HttpClient { get; private set; }

        static HttpRequestor()
        {
            HttpClient = new HttpClient();

            var timeout = PubgApiConfiguration.GetHttpTimeout();

            if (timeout.HasValue)
                HttpClient.Timeout = timeout.Value;
        }

        public static string GetString(string url, string apiToken)
        {
            var request = BuildRequest(url, apiToken);

            var response = HttpClient.SendAsync(request).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;

            return HandleResponse(response, responseContent);
        }

        public async static Task<string> GetStringAsync(string url, string apiToken, CancellationToken cancellationToken)
        {
            var request = BuildRequest(url, apiToken);

            var response = await HttpClient.SendAsync(request, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync();

            return HandleResponse(response, responseContent);
        }

        private static HttpRequestMessage BuildRequest(string url, string apiToken)
        {          
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (string.IsNullOrWhiteSpace(apiToken))
                throw new PubgUnauthorizedException();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            request.Headers.Add("Accept-Encoding", "gzip");

            return request;
        }

        private static string HandleResponse(HttpResponseMessage response, string responseContent)
        {
            if (response.IsSuccessStatusCode)
                return responseContent;

            throw BuildException(response, responseContent);
        }

        private static PubgException BuildException(HttpResponseMessage response, string responseContent)
        {
            switch(response.StatusCode)
            {
                case HttpStatusCode.Unauthorized: return new PubgUnauthorizedException();
                case HttpStatusCode.UnsupportedMediaType: return new PubgContentTypeException();
                default:
                    //TODO: review when we know what the response object will be like
                    return new PubgException(responseContent);
            }
        }
    }
}