
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherApi
{
    class GetWeather
    {
        private readonly string ApiKey;
        public GetWeather(string apiKey)
        {
            this.ApiKey = apiKey;
        }
        public async Task<Weather> CurrentWeather(string zipCode)
        {
            
            string url = $"https://api.weatherapi.com/v1/current.json?key={ApiKey}&q={zipCode}&aqi=no";

            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();
            JObject jo = JObject.Parse(json);
            Console.WriteLine(jo);

            //get the properties out of the JSON
            string city = (string)jo.SelectToken("location.name")!;
            string state = (string)jo.SelectToken("location.region")!;
            double lat = (double)jo.SelectToken("location.lat")!;
            double lon = (double)jo.SelectToken("location.lon")!;
            string decrip = (string)jo.SelectToken("current.condition.text")!;
            int temp = (int)jo.SelectToken("current.temp_f")!;


            Weather weather = new Weather(temp, decrip, city, state, lat, lon);            
            
            return weather;

        }
    }


}


