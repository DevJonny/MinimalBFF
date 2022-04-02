using Microsoft.AspNetCore.Mvc;
using MinimalBFF;
using MinimalBFF.Ports.Requests;
using Paramore.Darker;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfig();
builder.ConfigureDarker();
builder.ConfigureWeatherService();
    
var app = builder.Build();

app.MapGet("/weather", ([FromQuery] string city, [FromQuery] string country, [FromServices] IQueryProcessor queryProcessor) 
        => queryProcessor.ExecuteAsync(new WeatherRequest(city, country)).Result);

app.Run();
