using Newtonsoft.Json.Linq;

namespace ZmanimApi;

public class GetZmanim
{

    private readonly HttpClient httpClient;
    private readonly string apiKey;

    public GetZmanim(HttpClient httpClient, string apiKey)
    {
        this.httpClient = httpClient;
        this.apiKey = apiKey;
    }

    public async Task<Response> GetTodaysInfo(string address)
    {

        var location = await GetLocationDetails(address);
        var timezoneOffset = await GetTimeZoneOffset(location, GetSecondsSince1970());
        var zmanim = await GetTodaysZmanim(location.Latitude,location.Longitude, timezoneOffset);

        Response response = new Response(location, zmanim.ToStrings());

        return response;
    }

    public async Task<Location> GetLocationDetails(string address)
    {
        string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={apiKey}";

        HttpResponseMessage response = await httpClient.GetAsync(url);

        var json = await response.Content.ReadAsStringAsync();
        JObject o = JObject.Parse(json);
        Console.WriteLine(o.ToString());

        //get properties from JSON
        double latitude = (double)o.SelectToken("results[0].geometry.location.lat")!;
        double longitude = (double)o.SelectToken("results[0].geometry.location.lng")!;
        string formattedLocation = (string)o.SelectToken("results[0].formatted_address")!;
       
        var location = new Location(latitude, longitude , formattedLocation);

        return location;
    }


    public async Task<Zmanim> GetTodaysZmanim(double latitude, double longitude, int timezoneOffset)
    {
        string url = $"https://api.sunrise-sunset.org/json?lat={latitude}&lng={longitude}";

        HttpResponseMessage response = await httpClient.GetAsync(url);

        var json = await response.Content.ReadAsStringAsync();
        JObject o = JObject.Parse(json);
        Console.WriteLine(o.ToString());

        //get properties from JSON
        DateTime sunriseUtc = (DateTime)o.SelectToken("results.sunrise")!;
        DateTime sunsetUtc = (DateTime)o.SelectToken("results.sunset")!;

        DateTime sunrise = ConvertToOtherTimezone(sunriseUtc, timezoneOffset);
        DateTime sunset = ConvertToOtherTimezone(sunsetUtc, timezoneOffset);
        if(sunrise > sunset) sunset = sunset.AddDays(1);
       
        var zmanin = new Zmanim(sunrise, sunset);

        return zmanin;
    }

    public DateTime ConvertToOtherTimezone (DateTime dateTime, int timezoneOffset)
    {
        return dateTime.Add(TimeSpan.FromSeconds(timezoneOffset));
    }
    public async Task<int> GetTimeZoneOffset(Location location, long SecondsSince1970)
    {
        var url = $"https://maps.googleapis.com/maps/api/timezone/json?location={location.Latitude}%2C{location.Longitude}&timestamp={SecondsSince1970}&key={apiKey}";

        HttpResponseMessage response = await httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        Console.WriteLine(json);

        JObject o = JObject.Parse(json);
        string timeZoneName = (string)o.SelectToken("timeZoneName")!;
        int rawOffset = (int)o.SelectToken("rawOffset")!;
        int dstOffset = (int)o.SelectToken("dstOffset")!;

        
        return rawOffset + dstOffset;

    }
    public long GetSecondsSince1970()
    {
        DateTime now = DateTime.Now;
        DateTimeOffset epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        long SecondsSince1970 = (long)(now - epoch).TotalSeconds;

        return SecondsSince1970;
    }

}