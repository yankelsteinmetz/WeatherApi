using WeatherApi;

    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddJsonFile("appsettings.json");
    var app = builder.Build();

    app.MapGet("/", () =>
    {
        return "Hello World!";
    });

    app.MapGet("/{zipcode}/current", (string zipcode) =>
    {
        try
        {
            string apiKey = app.Configuration["WeatherApiKey"];
            var getWeather = new GetWeather(apiKey);
            var currentWeather =  getWeather.CurrentWeather(zipcode).Result;

    
            var getZmanim = new GetZmanim(currentWeather.Latitude, currentWeather.Longitude);
            var zmanim = getZmanim.TodaysZmanim().Result;


            return $@"
            {currentWeather.City}, {currentWeather.State} {zipcode}
            Weather: {currentWeather.Description} {currentWeather.Temperture}° F
            Sunrise: {zmanim.Sunrise.ToShortTimeString()}
            Sunset: {zmanim.Sunset.ToShortTimeString()}
            Sof Zman Krias Shema: {zmanim.SofZmanShema.ToShortTimeString()}
            Magan Avraham: {zmanim.SofZmanShemaMa.ToShortTimeString()}
            Sof Zman Tefilla: {zmanim.SofZmanTefilla.ToShortTimeString()}
            Magan Avraham: {zmanim.SofZmanTefillaMa.ToShortTimeString()}"; 

        }
        catch
        {
            return "Sorry, location not found";
        }
    });
    app.Run();
    