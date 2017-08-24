using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mews.Eet.Dto;

namespace Mews.Eet.Communication
{
    public class SoapHttpClient
    {
        public SoapHttpClient(Uri endpointUri, TimeSpan timeout, EetLogger logger)
        {
            EndpointUri = endpointUri;
            HttpClient = new HttpClient { Timeout = timeout };
            Logger = logger;
            EnableTls12();
        }

        private Uri EndpointUri { get; }

        private HttpClient HttpClient { get; }

        private EetLogger Logger { get; }

        public async Task<PostResponse> SendAsync(PostRequest request)
        {
            HttpClient.DefaultRequestHeaders.Add("SOAPAction", request.Operation);

            var requestContent = new StringContent(request.Body, Encoding.UTF8, "application/x-www-form-urlencoded");
            Logger?.Debug("Starting HTTP request.", new { HttpRequestBody = request.Body });

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var response = await HttpClient.PostAsync(EndpointUri, requestContent).ConfigureAwait(continueOnCapturedContext: false))
            {
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);

                stopwatch.Stop();
                var requestDuration = stopwatch.ElapsedMilliseconds;
                Logger?.Info($"HTTP request finished in {stopwatch.ElapsedMilliseconds}ms.", new { HttpRequestDuration = requestDuration });

                return new PostResponse(responseBody, request, requestDuration);
            }
        }

        private static void EnableTls12()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        }
    }
}