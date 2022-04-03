using MinimalBFF.Domain.Responses;
using MinimalBFF.Ports.Requests;

namespace MinimalBFF.Ports.Services;

public interface IWeatherService
{
    Task<IWeatherResponse> GetWeather(WeatherRequest weatherRequest);
}