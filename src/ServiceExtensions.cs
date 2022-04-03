using MinimalBFF.Adapters.OpenWeatherMap;
using MinimalBFF.Domain.Handlers;
using MinimalBFF.Ports;
using MinimalBFF.Ports.Attributes;
using MinimalBFF.Ports.Services;
using Paramore.Darker.AspNetCore;

namespace MinimalBFF;

public static class ServiceExtensions
{
    public static void AddConfig(this WebApplicationBuilder webApplicationBuilder)
    {
        var weatherSection = webApplicationBuilder.Configuration.GetSection("Weather");
        var weatherConfig = new WeatherConfig(weatherSection["ApiKey"]);
        var config = new Config(weatherConfig);

        webApplicationBuilder.Services.AddSingleton(config);
        webApplicationBuilder.Services.AddSingleton(weatherConfig);
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

    public static void ConfigureKestrel(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.WebHost
            .ConfigureKestrel(options => options.AddServerHeader = false);
    }
}