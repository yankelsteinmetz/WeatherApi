namespace WeatherApi;

public class Zmanim
{
    public Zmanim(DateTime sunrise, DateTime sunset, TimeSpan dayDuration)
    {
        this.Sunrise = sunrise;
        this.Sunset = sunset;
        this.DayDuration =  dayDuration;
        this.SofZmanShema = CalculateZmanShema();
        this.SofZmanShemaMa = CalculateZmanShemaMa();
        this.SofZmanTefilla = CalculateZmanTefilla();
        this.SofZmanTefillaMa = CalculateZmanTefillaMa();
        this.RabeinuTaam = CalculateRabeinuTaam();
    }

    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public DateTime SofZmanShema { get; set; }
    public DateTime SofZmanShemaMa { get; set; }
    public DateTime SofZmanTefilla { get; set; }
    public DateTime SofZmanTefillaMa { get; set; }
    public DateTime RabeinuTaam { get; set; }
    public TimeSpan DayDuration {get; set; }

    public DateTime CalculateZmanShema()
    {
        TimeSpan threeHours = DayDuration.Divide(4);
        return Sunrise.Add(threeHours);
    }
    public DateTime CalculateZmanTefilla()
    {
        TimeSpan fourHours = DayDuration.Divide(3);
        return Sunrise.Add(fourHours);
    }
    public DateTime CalculateZmanShemaMa()
    {
        return SofZmanShema.Subtract(TimeSpan.FromMinutes(36));
    }
    public DateTime CalculateZmanTefillaMa()
    {
        return SofZmanTefilla.Subtract(TimeSpan.FromMinutes(24));
    }
    public DateTime CalculateRabeinuTaam()
    {
        return Sunset.Add(TimeSpan.FromMinutes(72));
    }
}