using MinimalBFF.Ports;
using MinimalBFF.Ports.Requests;
using MinimalBFF.Ports.Services;

namespace MinimalBFF.Adapters.OpenWeatherMap;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly Config _config;

    public WeatherService(HttpClient httpClient, Config config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<HttpResponseMessage> GetWeather(WeatherRequest weatherRequest)
    {
        var response = await _httpClient
            .GetAsync($"q={weatherRequest.City},{weatherRequest.GetHashCode()}&APPID={_config.Weather.ApiKey}");
        
        return response;
    }
}