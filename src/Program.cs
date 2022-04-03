using Microsoft.AspNetCore.Mvc;
using MinimalBFF;
using MinimalBFF.Ports.Requests;
using Paramore.Darker;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfig();
builder.ConfigureDarker();
builder.ConfigureWeatherService();
    
var app = builder.Build();

app.MapGet("/weather", async ([FromQuery] float? lon, [FromQuery] float? lat, [FromServices] IQueryProcessor queryProcessor) 
        => (await queryProcessor.ExecuteAsync(new WeatherRequest(lon, lat))).Result);

app.Run();
