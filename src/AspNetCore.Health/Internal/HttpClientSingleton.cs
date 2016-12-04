using System;
using System.Net.Http;

namespace AspNetCore.Health.Internal
{
    public class HttpClientSingleton : HttpClient
    {
        private static readonly Lazy<HttpClientSingleton> Lazy =
        new Lazy<HttpClientSingleton>(() => new HttpClientSingleton());

        public static HttpClientSingleton Instance => Lazy.Value;

        private HttpClientSingleton()
        {
        }
    }
}
