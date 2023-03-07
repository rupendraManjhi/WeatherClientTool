using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherClientTool
{
    class WeatherLocation
    {
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public decimal generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public string elevation { get; set; }
        public Current_Weather current_weather { get; set; }
    }
    public class Current_Weather
    {
        public decimal temperature { get; set; }
        public decimal windspeed { get; set; }
        public decimal winddirection { get; set; }
        public int weathercode { get; set; }
        public DateTime time { get; set; }
       
    }
}
