namespace ZmanimApi;

public class Zmanim
{
    public Zmanim (DateTime sunrise, DateTime sunset)
    {
        this.Neitz = sunrise;
        this.Shkiah = sunset;
        this.dayDuration =  CalculateDayDuration();
        this.Alos = CalculateAlos();
        this.RabeinuTaam = CalculateRabeinuTaam();
        this.dayDurationMa =  CalculateDayDurationMa();
        this.SofZmanShema = CalculateZmanShema();
        this.SofZmanShemaMa = CalculateZmanShemaMa();
        this.SofZmanTefilla = CalculateZmanTefilla();
        this.SofZmanTefillaMa = CalculateZmanTefillaMa();
    }

    public DateTime Alos { get; }
    public DateTime Neitz { get; }
    public DateTime Shkiah {get;}
    public DateTime SofZmanShema { get;}
    public DateTime SofZmanShemaMa { get;}
    public DateTime SofZmanTefilla { get; }
    public DateTime SofZmanTefillaMa { get; }
    public DateTime RabeinuTaam { get; }
    private TimeSpan dayDuration;
    private TimeSpan dayDurationMa;


    private TimeSpan CalculateDayDuration()
    {
        return Shkiah - Neitz;
    }
    private TimeSpan CalculateDayDurationMa()
    {
        return RabeinuTaam - Alos;
    }
    private DateTime CalculateZmanShema()
    {
        TimeSpan threeHours = dayDuration.Divide(4);
        return Neitz.Add(threeHours);
    }
    private DateTime CalculateZmanTefilla()
    {
        TimeSpan fourHours = dayDuration.Divide(3);
        return Neitz.Add(fourHours);
    }
    private DateTime CalculateZmanShemaMa()
    {
        TimeSpan threeHours = dayDurationMa.Divide(4);
        return Alos.Add(threeHours);
    }
    private DateTime CalculateZmanTefillaMa()
    {
        TimeSpan fourHours = dayDurationMa.Divide(3);
        return Alos.Add(fourHours);
    }
    private DateTime CalculateRabeinuTaam()
    {
        return Shkiah.Add(TimeSpan.FromMinutes(72));
    }
    private DateTime CalculateAlos()
    {
         return Neitz.Subtract(TimeSpan.FromMinutes(72));
    }

    public Zmanim_times ToStrings()
    {
        return new Zmanim_times(this);
    }

}   

public class Zmanim_times
{
    public Zmanim_times(Zmanim zmanim)
    {
        Alos = zmanim.Alos.ToShortTimeString();
        Neitz = zmanim.Neitz.ToShortTimeString();
        Shkiah = zmanim.Shkiah.ToShortTimeString();
        SofZmanShema = zmanim.SofZmanShema.ToShortTimeString();
        SofZmanShemaMa = zmanim.SofZmanShemaMa.ToShortTimeString();
        SofZmanTefilla = zmanim.SofZmanTefilla.ToShortTimeString();
        SofZmanTefillaMa = zmanim.SofZmanTefillaMa.ToShortTimeString();
        RabeinuTaam = zmanim.RabeinuTaam.ToShortTimeString();
    }

    public string Alos { get; }
    public string Neitz { get; }
    public string Shkiah { get; }
    public string SofZmanShema { get;}
    public string SofZmanShemaMa { get;}
    public string SofZmanTefilla { get; }
    public string SofZmanTefillaMa { get; }
    public string RabeinuTaam { get; }
}