using MinimalBFF.Adapters.OpenWeatherMap;
using MinimalBFF.Ports.Requests;

namespace MinimalBFF.Ports.Services;

public interface IWeatherService
{
    Task<HttpResponseMessage> GetWeather(WeatherRequest weatherRequest);
}