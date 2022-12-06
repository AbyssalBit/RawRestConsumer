using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using SteelPython;

namespace RequestGeneralApi
{
    internal class Program
    {
        private static readonly string fullUri = "https://devmotorgv.azurewebsites.net/api/turno";
        private static readonly double TimeOut = 1;
        static async Task Main(string[] args)
        {

            Dictionary<string, string> headersDict = new Dictionary<string, string>();
            headersDict.Add("authorization", "no");
            // headersDict.Add("Content-type", "application/json");
            string content = "{\"IdEmpresa\": 5702}";
            // CustomPythonEngine _customPythonEngine = new CustomPythonEngine();
            CustomPythonEngine.RunPython();
            
            // await Request(fullUri, headersDict, content);



        }
        static async Task Request(string apiUri, Dictionary<string, string> headersDict, string content) 
        {
            var client = new HttpClient();
            // client.DefaultRequestHeaders = new StringContent(header, Encoding.UTF8, "application/json");
            // client.DefaultRequestHeaders.Add("ApiKey", key);
            foreach (KeyValuePair<string, string> element in headersDict)
            {
                // Console.WriteLine("Key: {0}, Value: {1}", element.Key, element.Value);
                client.DefaultRequestHeaders.Add(element.Key, element.Value);
            }
            // var json = JsonConvert.SerializeObject(content);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(apiUri),
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(body);
            // var deserialized_response = JsonConvert.DeserializeObject<dynamic>(body);
            dynamic stuff = JsonConvert.DeserializeObject<dynamic>(body);
            // string ans = (string)(deserialized_response);
            Console.WriteLine(stuff);
        }
    }
}
