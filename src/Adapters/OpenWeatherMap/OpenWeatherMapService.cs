using System.Text.Json;
using MinimalBFF.Domain.Responses;
using MinimalBFF.Ports;
using MinimalBFF.Ports.Requests;
using MinimalBFF.Ports.Services;

namespace MinimalBFF.Adapters.OpenWeatherMap;

public class OpenWeatherMapService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly Config _config;

    public OpenWeatherMapService(HttpClient httpClient, Config config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<IWeatherResponse> GetWeather(WeatherRequest weatherRequest)
    {
        var response = await _httpClient
            .GetAsync($"https://api.openweathermap.org/data/2.5/onecall?lon={weatherRequest.Lon}&lat={weatherRequest.Lat}&appid={_config.Weather.ApiKey}");

        response.EnsureSuccessStatusCode();

        var openWeatherMapResponse = await response.Content.ReadFromJsonAsync<OpenWeatherMapResponse>(new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        var currentWeather = openWeatherMapResponse?.CurrentWeather ?? new OpenWeatherMapResponse().CurrentWeather;
        
        return new WeatherResponse
        {
            Forecast = currentWeather.WeatherDescription.First().Main,
            Temp = currentWeather.Temp
        };
    }
}