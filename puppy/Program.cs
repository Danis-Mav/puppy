// OpenWeatherMap REST API example
// DKY 2021

using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenWeather
{
    class Program
    {
        static void Main(string[] args)
        {
            var ingredients = "onions,garlic";
            var query = "omelet";
            var page = "3";

            var url = $"http://www.recipepuppy.com/api/?i={ingredients}&q={query}&p={page}";

            var request = WebRequest.Create(url);

            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return;
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                Console.WriteLine(result);
               var weatherForecast = JsonConvert.DeserializeObject<Root>(result);
               Console.WriteLine(weatherForecast.results);
            }

        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Result
    {
        public string title { get; set; }
        public string href { get; set; }
        public string ingredients { get; set; }
        public string thumbnail { get; set; }
    }

    public class Root
    {
        public string title { get; set; }
        public double version { get; set; }
        public string href { get; set; }
        public List<Result> results { get; set; }
    }
}
