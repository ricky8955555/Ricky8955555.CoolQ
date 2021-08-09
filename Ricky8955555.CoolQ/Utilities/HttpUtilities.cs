using System;
using System.IO;
using System.Net.Http;

namespace Ricky8955555.CoolQ
{
    public static class HttpUtilities
    {
        public static HttpResponseMessage HttpGet(Uri uri) => new HttpClient().GetAsync(uri).Result;

        public static HttpResponseMessage HttpGet(string uri) => new HttpClient().GetAsync(uri).Result;

        public static bool HttpGet(Uri uri, out HttpContent content)
        {
            var res = HttpGet(uri);
            content = HttpGet(uri).Content;
            return res.IsSuccessStatusCode;
        }

        public static bool HttpGet(string uri, out HttpContent content)
        {
            var res = HttpGet(uri);
            content = HttpGet(uri).Content;
            return res.IsSuccessStatusCode;
        }

        public static bool HttpGet(Uri uri, out string content)
        {
            var res = HttpGet(uri);
            content = ReadContentAsString(HttpGet(uri).Content);
            return res.IsSuccessStatusCode;
        }

        public static bool HttpGet(string uri, out string content)
        {
            var res = HttpGet(uri);
            content = ReadContentAsString(HttpGet(uri).Content);
            return res.IsSuccessStatusCode;
        }

        public static bool HttpDownload(Uri uri, string fileName)
        {
            if (HttpGet(uri, out HttpContent content))
            {
                File.WriteAllBytes(fileName, content.ReadAsByteArrayAsync().Result);
                return true;
            }
            else
                return false;
        }

        public static bool HttpDownload(string uri, string fileName)
        {
            if (HttpGet(uri, out HttpContent content))
            {
                File.WriteAllBytes(fileName, content.ReadAsByteArrayAsync().Result);
                return true;
            }
            else
                return false;
        }

        public static string ReadContentAsString(HttpContent content) => content.ReadAsStringAsync().Result;
    }
}