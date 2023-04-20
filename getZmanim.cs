using Newtonsoft.Json.Linq;

namespace WeatherApi;

public class GetZmanim
{
    public GetZmanim(double lat, double lon)
    {
        this.Latitude = lat;
        this.Longitude = lon;
    }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public async Task<Zmanim> TodaysZmanim()
    {
        string url = $"https://api.sunrise-sunset.org/json?lat={Latitude}&lng={Longitude}";

        var httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync(url);

        var json = await response.Content.ReadAsStringAsync();
        JObject o = JObject.Parse(json);
        Console.WriteLine(o.ToString());

        //get properties from JSON
        string sunriseString = (string)o.SelectToken("results.sunrise")!;
        string sunsetString = (string)o.SelectToken("results.sunset")!;
        string dayDurationString = (string)o.SelectToken("results.day_length")!;

        //Convert to Date Time / Timespan
        var sunrise = DateTime.Parse(sunriseString).ToLocalTime();
        var sunset = DateTime.Parse(sunsetString).ToLocalTime();
        var dayDuration = TimeSpan.Parse(dayDurationString);

       
        var zmanin = new Zmanim(sunrise, sunset, dayDuration);

        return zmanin;
    }
}