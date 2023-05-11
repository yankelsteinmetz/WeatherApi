namespace ZmanimApi;

public class Response
{
    public Results Results {get; set; } =new Results();
    public int Status {get; set; }
}

public class Results
{
    public Location? Location {get; set;}

    public Zmanim_times? Zmanim {get; set; }

}