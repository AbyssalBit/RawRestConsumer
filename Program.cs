using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace RequestGeneralApi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://bloomberg-market-and-financial-news.p.rapidapi.com/market/auto-complete?query=%3CREQUIRED%3E"),
                Headers =
                {
                    { "X-RapidAPI-Key", "086aae3746mshdb3f3ea0c68f17ap18bd08jsnbfa1b20c2539" },
                    { "X-RapidAPI-Host", "bloomberg-market-and-financial-news.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
    }
}
