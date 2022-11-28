using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace RequestGeneralApi
{
    internal class Program
    {
        private static readonly string fullUri = "http://127.0.0.1:8000/testpost";
        private static readonly double TimeOut = 1;
        static async Task Main(string[] args)
        {
            // string header = @"{
            //                 { 'X-RapidAPI-Key', '086aae3746mshdb3f3ea0c68f17ap18bd08jsnbfa1b20c2539' },
            //                 { 'X-RapidAPI-Host', 'google-translate1.p.rapidapi.com' },
            //                 }";


            // Dictionary<string, string> Headers = {
            //                 { "X-RapidAPI-Key", "086aae3746mshdb3f3ea0c68f17ap18bd08jsnbfa1b20c2539" },
            //                 { 'X-RapidAPI-Host', 'google-translate1.p.rapidapi.com' },
            //                 };


            Dictionary<string, string> headersDict = new Dictionary<string, string>();
            headersDict.Add("X-RapidAPI-Key", "086aae3746mshdb3f3ea0c68f17ap18bd08jsnbfa1b20c2539");
            headersDict.Add("X-RapidAPI-Host", "google-translate1.p.rapidapi.com");
            string content = @"{
                { 'q', 'English is hard, but detectably so' },
            }";
            
            await Request(fullUri, headersDict, content);

        }
        static async Task Request(string apiUri, Dictionary<string, string> headersDict, string content) 
        {
            
            var client = new HttpClient();
            // client.DefaultRequestHeaders = new StringContent(header, Encoding.UTF8, "application/json");
            // client.DefaultRequestHeaders.Add("ApiKey", key);
            foreach (KeyValuePair<string, string> element in headersDict)
            {
                Console.WriteLine("Key: {0}, Value: {1}", element.Key, element.Value);
                client.DefaultRequestHeaders.Add(element.Key, element.Value);
            }
            var json = JsonConvert.SerializeObject(content);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(apiUri),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            // request.Headers = new StringContent(header, Encoding.UTF8, "application/json");



            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var deserialized_response = JsonConvert.DeserializeObject<dynamic>(body);
            string ans = (string)(deserialized_response);

            Console.WriteLine(ans);



        

        }
    }
}
