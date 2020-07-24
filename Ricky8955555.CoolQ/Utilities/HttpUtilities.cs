using System.Net.Http;

namespace Ricky8955555.CoolQ
{
    internal static class HttpUtilities
    {
        internal static HttpResponseMessage HttpGet(string uri) => new HttpClient().GetAsync(uri).Result;

        internal static bool HttpGet(string uri, out HttpContent content)
        {
            var res = HttpGet(uri);
            content = HttpGet(uri).Content;
            return res.IsSuccessStatusCode;
        }

        internal static bool HttpGet(string uri, out string content) {
            var res = HttpGet(uri);
            content = ReadContentAsString(HttpGet(uri).Content);
            return res.IsSuccessStatusCode;
        }

        internal static string ReadContentAsString(HttpContent content) => content.ReadAsStringAsync().Result;
    }
}