namespace ZmanimApi;

public class Response
{
    public Response(Location location, Zmanim_times zmanim)
    {
        Location = location;
        Zmanim = zmanim;
    }

    public Location Location {get; set;}
    public Zmanim_times Zmanim {get; set; }
    public string? Error {get; set;}
}
