namespace WeatherApi;
public class Weather
{
    public Weather(int temp, string desc, string city,string state, double lat, double lon)
    {
        this.Temperture =temp;
        this.Description =desc;
        this.City= city;
        this.State =state;
        this.Latitude = lat;
        this.Longitude =lon;
    }
    public int Temperture { get; set; }
    public string Description { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

}

