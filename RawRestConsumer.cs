using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using Helpers;

namespace RequestGeneralApi
{
    internal sealed class RawRestConsumer
        {
            private static readonly double TimeOut = "1";
            private static readonly string BaseApiUrl = "";
            private static readonly string AuthorizationKey = "";
            private readonly HttpClient Client = null;
            // private static readonly HttpClient MotorCalculoRestClient = null;
            // private static readonly HttpClient RawDataRestClient = null;

            static RestConsumer()
                {
                    Client = new HttpClient
                        {
                            BaseAddress = new Uri(BaseApiUrl),
                            Timeout = TimeSpan.FromMinutes(TimeOut)
                        };
            }
            public RestConsumer(string ApiUrl, string AuthorizationKey)
            {
                Client = new HttpClient
                    {
                        BaseAddress = new Uri(BaseApiUrl),
                        Timeout = TimeSpan.FromMinutes(TimeOut)
                    };
                Client.DefaultRequestHeaders.Add("authorization", AuthorizationKey);
                
            }
            public T GetResponse<T, U>(string url, U obj)
            {
                T ret = default;

                HttpResponseMessage response = null;

                RetryHelper.Execute(
                    () =>
                    {
                        response = AsyncHelper.RunSync<HttpResponseMessage>(() => Client.PostAsync(url, CreateHttpContent<U>(obj)));
                        response.EnsureSuccessStatusCode();
                    }
                );


                Console.WriteLine(response);

                return ret;
            }

            public string GetStringResponse<T>(string url, T obj)
            {


                HttpResponseMessage response = null;

                RetryHelper.Execute(
                    () =>
                    {
                        response = AsyncHelper.RunSync<HttpResponseMessage>(() => Client.PostAsync(url, CreateHttpContent<T>(obj)));
                        response.EnsureSuccessStatusCode();
                    }
                );


                if (response.IsSuccessStatusCode)
                {
                    string resp = AsyncHelper.RunSync<string>(() => response.Content.ReadAsStringAsync());

                    dynamic stuff = JsonConvert.DeserializeObject<dynamic>(resp);


                    return resp;
                    // return JsonConvert.DeserializeObject<T>(resp);
                }

                return "error";
                //return ret;
            }

            private static void bossaIterate(dynamic variable, int indent)
            {


                if (variable.GetType() == typeof(Newtonsoft.Json.Linq.JObject))
                {

                    foreach (var property in variable)
                    {
                        for (int i = 0; i < indent; i++)
                        {
                            System.Diagnostics.Debug.Write("_");
                        }


                        bossaIterate(property.Value, indent + 1);

                    }
                }
                else if (variable.GetType() == typeof(Newtonsoft.Json.Linq.JArray))
                {

                    System.Diagnostics.Debug.WriteLine("");
                    foreach (var item in variable)
                    {
                        for (int i = 0; i < indent; i++)
                        {
                            System.Diagnostics.Debug.Write("_");
                        }
                        bossaIterate(item, indent + 1);
                    }
                }
                else if (variable.GetType() == typeof(Newtonsoft.Json.Linq.JValue))
                {
                    //System.Diagnostics.Debug.Write("#"+variable.ToString()+"#");

                    //Console.WriteLine("type is Variable, value: " + variable.ToString());
                }
            }
            private HttpContent CreateHttpContent<T>(T content)
            {
                var json = JsonConvert.SerializeObject(content);
                return new StringContent(json, Encoding.UTF8, "application/json");
            }

        }
    
    }
