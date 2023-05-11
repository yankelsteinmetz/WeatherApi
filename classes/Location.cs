namespace ZmanimApi;

public class Location
{
    public Location(double latitude, double longitude, string formattedLocation)
    {
        Latitude = latitude;
        Longitude = longitude;
        FormattedLocation = formattedLocation;
    }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public string FormattedLocation { get; set; }

}