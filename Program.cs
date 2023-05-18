using Newtonsoft.Json;
using ZmanimApi;

    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddJsonFile("appsettings.json");
    builder.Services.AddSingleton<HttpClient>();

    var app = builder.Build();
    app.UseStaticFiles("/wwwroot");


    app.MapGet("/", () =>
    {
        var html = File.ReadAllText("./wwwroot/index.html");
        return Microsoft.AspNetCore.Http.Results.Content(html, "text/html");
    });

    app.MapGet("/{zipcode}/current", async (string zipcode) =>
    {
        
        string apiKey = app.Configuration["GoogleMapsApiKey"];

        var getZmanim = new GetZmanim(app.Services.GetRequiredService<HttpClient>(),apiKey);
        var response = await getZmanim.GetTodaysInfo(zipcode);

        return Results.Ok(response);
    
    });

    app.MapGet("/{anythingElse}", () =>
    {
        Results.NotFound("404 Not Found");
    });
    app.Run();
    