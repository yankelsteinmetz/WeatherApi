using Newtonsoft.Json.Linq;

namespace ZmanimApi;

public class GetZmanim
{

    private readonly HttpClient _httpClient;

    public GetZmanim(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response> GetTodaysInfo(string address, string apiKey)
    {
        Response response =new Response();

        response.Results.Location = await GetLocationDetails(address,apiKey);
        var zmanim = await GetTodaysZmanim(response.Results.Location.Latitude, response.Results.Location.Longitude);
        response.Results.Zmanim = zmanim.ToStrings();
        response.Status = 200;

        return response;
    }

    public async Task<Location> GetLocationDetails(string address, string apiKey)
    {
        string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={apiKey}";

        HttpResponseMessage response = await _httpClient.GetAsync(url);

        var json = await response.Content.ReadAsStringAsync();
        JObject o = JObject.Parse(json);
        Console.WriteLine(o.ToString());

        //get properties from JSON
        double latitude = (double)o.SelectToken("results[0].geometry.location.lat")!;
        double longitude = (double)o.SelectToken("results[0].geometry.location.lng")!;
        string formattedLocation = (string)o.SelectToken("results[0].formatted_address")!;
       
       Console.WriteLine(latitude);
       Console.WriteLine(longitude);
       Console.WriteLine(formattedLocation);
        var location = new Location(latitude, longitude , formattedLocation);

        return location;
    }


    public async Task<Zmanim> GetTodaysZmanim(double latitude, double longitude)
    {
        string url = $"https://api.sunrise-sunset.org/json?lat={latitude}&lng={longitude}";

        HttpResponseMessage response = await _httpClient.GetAsync(url);

        var json = await response.Content.ReadAsStringAsync();
        JObject o = JObject.Parse(json);
        Console.WriteLine(o.ToString());

        //get properties from JSON
        DateTime sunriseUtc = (DateTime)o.SelectToken("results.sunrise")!;
        DateTime sunsetUtc = (DateTime)o.SelectToken("results.sunset")!;

        DateTime sunrise = ConvertTimezone(sunriseUtc, "Eastern Standard Time");
        DateTime sunset = ConvertTimezone(sunsetUtc, "Eastern Standard Time");
       
        var zmanin = new Zmanim(sunrise, sunset);

        return zmanin;
    }

    public DateTime ConvertTimezone (DateTime dateTime, string timeZone)
    {
        //get the number of milliseconds since 1970
        DateTime now = DateTime.Now;
        DateTimeOffset epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        long millisecsSince1970 = (long)(now - epoch).TotalMilliseconds;

        var url = $"https://maps.googleapis.com/maps/api/timezone/json?location={39.6034810}%2C{-119.6822510}&timestamp={millisecsSince1970}&key={"YOUR_API_KEY"}";

        //convert the date to the correct time zone
        TimeZoneInfo utcZone = TimeZoneInfo.Utc;
        TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
        DateTime currentTimeZone = TimeZoneInfo.ConvertTime(dateTime, utcZone, easternZone);

        return currentTimeZone;

    }
}