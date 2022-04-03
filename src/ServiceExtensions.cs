using MinimalBFF.Adapters.OpenWeatherMap;
using MinimalBFF.Domain.Handlers;
using MinimalBFF.Ports;
using MinimalBFF.Ports.Attributes;
using MinimalBFF.Ports.Services;
using Paramore.Darker.AspNetCore;
using Weather = MinimalBFF.Ports.Weather;

namespace MinimalBFF;

public static class ServiceExtensions
{
    public static void AddConfig(this WebApplicationBuilder webApplicationBuilder)
    {
        var weatherSection = webApplicationBuilder.Configuration.GetSection("Weather");
        var config = new Config(new Weather(weatherSection["ApiKey"]));

        webApplicationBuilder.Services.AddSingleton(config);
    }
    
    public static void ConfigureDarker(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services
            .AddDarker()
            .AddHandlersFromAssemblies(typeof(GetWeatherHandler).Assembly)
            .RegisterDecorator(typeof(ResultConverterDecorator<,>));
    }

    public static void ConfigureWeatherService(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services
            .AddHttpClient<IWeatherService, OpenWeatherMapService>();
    }
}