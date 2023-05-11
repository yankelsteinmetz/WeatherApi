using Newtonsoft.Json;
using ZmanimApi;

    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddJsonFile("appsettings.json");
    builder.Services.AddSingleton<HttpClient>();

    var app = builder.Build();

    app.MapGet("/", () =>
    {
        var html = File.ReadAllText("./front-end/index.html");
        return Microsoft.AspNetCore.Http.Results.Content(html, "text/html");
    });

    app.MapGet("/{zipcode}/current", async (string zipcode) =>
    {
        
        string apiKey = app.Configuration["GoogleMapsApiKey"];

        var getZmanim = new GetZmanim(app.Services.GetRequiredService<HttpClient>());
        var response = await getZmanim.GetTodaysInfo(zipcode,apiKey);

        return JsonConvert.SerializeObject(response);
    
    });

    app.MapGet("/{anythingElse}", () =>
    {
        return "404 Page Not Found";
    });
    app.Run();
    