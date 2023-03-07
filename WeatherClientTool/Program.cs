using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace WeatherClientTool
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            decimal lat=0, lang=0;
            string Baseurl = "";
            string Inpcity = "Delhi";
            string fileName = "Country.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            WeatherLocation weather = new WeatherLocation();
            var json = File.ReadAllText(path);
            List<location> loc;
            loc = JsonConvert.DeserializeObject<List<location>>(json);
            Console.WriteLine("Enter the City Name : ");
            Inpcity = Console.ReadLine();
            
            location newLocation = new location();
            
            foreach(var childLoc in loc)
            {
                if (childLoc.city.ToLower() == Inpcity.ToLower())
                {
                    newLocation = childLoc;
                    break;
                }
                
             }
            if (newLocation.city != "")
            {
                if (newLocation.lat != 0)
                    lat = newLocation.lat;

                if (newLocation.lng != 0)
                    lang = newLocation.lng;

                using(var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");
                    //HttpResponseMessage responseTask = await client.GetAsync(RequestUri);
                    Baseurl = "https://api.open-meteo.com/v1/forecast?latitude="+lat+"&longitude="+lang+"&current_weather=true";
                    var responseTask = client.GetAsync(Baseurl);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        var WeatherLocationResult = readTask.Result;
                        WeatherLocation weahter = JsonConvert.DeserializeObject<WeatherLocation>( WeatherLocationResult);
                        
                        Console.WriteLine("Current Weather Temperature: "+ weahter.current_weather.temperature);
                        Console.WriteLine("Current Weather Time: "+ weahter.current_weather.time);
                        Console.WriteLine("Current Weather WeatherCode: " +weahter.current_weather.weathercode);
                        Console.WriteLine("Current Weather Wind Direction: "+ weahter.current_weather.winddirection);
                        Console.WriteLine("Current Weather Wind Speed: "+ weahter.current_weather.windspeed);

                    }

                }
            }
           


        }
    }
}
