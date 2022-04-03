using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using MinimalBFF.Domain.Responses;
using MinimalBFF.Ports;
using MinimalBFF.Ports.Requests;
using MinimalBFF.Ports.Services;

namespace MinimalBFF.Adapters.OpenWeatherMap;

public class OpenWeatherMapService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly WeatherConfig _weatherConfig;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

    public OpenWeatherMapService(HttpClient httpClient, WeatherConfig weatherConfig)
    {
        _httpClient = httpClient;
        _weatherConfig = weatherConfig;
    }

    public async Task<IWeatherResponse> GetWeather(WeatherRequest weatherRequest)
    {
        var response = await _httpClient
            .GetAsync($"https://api.openweathermap.org/data/2.5/onecall?lat={weatherRequest.Lat}&lon={weatherRequest.Lon}&appid={_weatherConfig.ApiKey}");
        
        await HandleErrorCodes(response);

        var openWeatherMapResponse = await ParseJsonResponse<OpenWeatherMapResponse>(response);

        var currentWeather = openWeatherMapResponse?.CurrentWeather ?? new OpenWeatherMapResponse().CurrentWeather;
        
        return new WeatherResponse
        {
            Forecast = currentWeather.WeatherDescription.First().Main,
            Temp = currentWeather.Temp
        };
    }

    private async Task HandleErrorCodes(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;
        
        if (response.StatusCode is not HttpStatusCode.BadRequest)
            response.EnsureSuccessStatusCode();
        else
        {
            var errorResponse = await ParseJsonResponse<OpenWeatherMapErrorResponse>(response);
            throw new ValidationException(errorResponse.Message);
        }
    }

    private async Task<T> ParseJsonResponse<T>(HttpResponseMessage response) => await response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions);
}