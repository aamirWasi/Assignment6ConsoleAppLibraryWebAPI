using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using WebAPI.Core;

namespace Assignment6ConsoleAppLibraryWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Product to show from API to Console
            Console.ReadLine();
            var product = new Product()
            {
                Name = "Huawei MateBook D",
                Price = 78000
            };
            ShowDataApproach();
            SaveProduct(product);
        }
        //Console to API
        private static void SaveProduct(Product product)
        {
            const string url = "https://localhost:44320/api/products";
            var request = WebRequest.Create(url);
            request.Method = "Post";
            request.ContentType = "application/json";
            var requestContent = JsonConvert.SerializeObject(product);
            var data = Encoding.UTF8.GetBytes(requestContent);
            request.ContentLength = data.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
                requestStream.Flush();
                using (var response = request.GetResponse())
                {
                    using (var streamItem = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(streamItem))
                        {
                            var result = reader.ReadToEnd();
                            Console.WriteLine($"{result}");
                        }
                    }
                }
            }
        }
        //API to Console
        private static void ShowDataApproach()
        {

            const string url = "https://localhost:44320/api/products";
            var request = WebRequest.Create(url);
            request.Method = "Get";
            request.ContentType = "application/json";
            using (var response = request.GetResponse())
            {
                using (var streamItem = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(streamItem))
                    {
                        var data = reader.ReadToEnd();
                        dynamic result = JsonConvert.DeserializeObject(data);
                        var count = 1;
                        foreach (var item in result)
                        {
                            Console.WriteLine($"Item{count} : {item}");
                            count++;
                        }
                    }
                }
            }
        }
    }
}
