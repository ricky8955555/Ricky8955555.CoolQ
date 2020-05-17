using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HuajiTech.QQ;

namespace Ricky8955555.CoolQ.Tools
{
    static class HttpGetTool
    {

        public static (bool IsSuccessful, string Result) GetAndCheckIsSuccessful(string url)
        {
            var client = new HttpClient();
            try
            {
                var res = client.GetAsync(url).Result;
                if (res.IsSuccessStatusCode)
                    return (true, res.Content.ReadAsStringAsync().Result);
            }
            catch
            {
                return (false, null);
            }
            return (false, null);
        }

        public static (bool IsSuccessful, string[] Result) GetAndCheckIsSuccessful(string url, string header)
        {
            var client = new HttpClient();
            try
            {
                var res = client.GetAsync(url).Result;
                if (res.IsSuccessStatusCode)
                    return (true, res.Headers.GetValues(header).ToArray());
            }
            catch
            {
                return (false, null);
            }
            return (false, null);
        }

        public static (bool IsSuccessful, IEnumerable<string>[] Result) GetAndCheckIsSuccessful(string url, string[] headers)
        {
            var client = new HttpClient();
            try
            {
                var res = client.GetAsync(url).Result;
                if (res.IsSuccessStatusCode)
                    return (true, headers.Select(x => res.Headers.GetValues(x)).ToArray());
            }
            catch
            {
                return (false, null);
            }
            return (false, null);
        }

        public static (bool IsSuccessful, string Result) GetAndCheckIsSuccessful(string url, string name, string value)
        {
            try
            {
                var handler = new HttpClientHandler() { UseCookies = false };
                var client = new HttpClient(handler);
                var msg = new HttpRequestMessage(HttpMethod.Get, url);

                msg.Headers.Add(name, value);

                var res = client.SendAsync(msg).Result;

                if (res.IsSuccessStatusCode)
                    return (true, res.Content.ReadAsStringAsync().Result);
            }
            catch
            {
                return (false, null);
            }
            return (false, null);
        }

        public static (bool IsSuccessful, string Result) GetAndCheckIsSuccessful(string url, string name, IEnumerable<string> value)
        {
            try
            {
                var handler = new HttpClientHandler() { UseCookies = false };
                var client = new HttpClient(handler);
                var msg = new HttpRequestMessage(HttpMethod.Get, url);

                msg.Headers.Add(name, value);

                var res = client.SendAsync(msg).Result;

                if (res.IsSuccessStatusCode)
                    return (true, res.Content.ReadAsStringAsync().Result);
            }
            catch
            {
                return (false, null);
            }
            return (false, null);
        }

        public static (bool IsSuccessful, string Result) GetAndCheckIsSuccessful(string url, (string Name, string Value)[] headers)
        {
            try
            {
                var handler = new HttpClientHandler() { UseCookies = false };
                var client = new HttpClient(handler);
                var msg = new HttpRequestMessage(HttpMethod.Get, url);

                foreach ((string name, string value) in headers)
                    msg.Headers.Add(name, value);

                var res = client.SendAsync(msg).Result;

                if (res.IsSuccessStatusCode)
                    return (true, res.Content.ReadAsStringAsync().Result);
            }
            catch
            {
                return (false, null);
            }
            return (false, null);
        }

        public static (bool IsSuccessful, string Result) GetAndCheckIsSuccessful(string url, (string Name, IEnumerable<string> Value)[] headers)
        {
            try
            {
                var handler = new HttpClientHandler() { UseCookies = false };
                var client = new HttpClient(handler);
                var msg = new HttpRequestMessage(HttpMethod.Get, url);

                foreach ((string name, IEnumerable<string> value) in headers)
                    msg.Headers.Add(name, value);

                var res = client.SendAsync(msg).Result;

                if (res.IsSuccessStatusCode)
                    return (true, res.Content.ReadAsStringAsync().Result);
            }
            catch
            {
                return (false, null);
            }
            return (false, null);
        }
    }
}
