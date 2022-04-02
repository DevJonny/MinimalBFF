using MinimalBFF.Adapters.OpenWeatherMap;
using MinimalBFF.Domain.Responses;
using MinimalBFF.Ports.Attributes;
using MinimalBFF.Ports.Requests;
using MinimalBFF.Ports.Services;
using Paramore.Darker;

namespace MinimalBFF.Domain.Handlers;

public class GetWeatherHandler : QueryHandlerAsync<WeatherRequest, WeatherResponse>
{
    private readonly IWeatherService _weatherService;

    public GetWeatherHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpStatusToResultConverter.HttpStatusToResultConverterAttribute(1)]
    public override async Task<WeatherResponse> ExecuteAsync(WeatherRequest query, CancellationToken cancellationToken = new())
    {
        var weatherResponse = await _weatherService.GetWeather(new WeatherRequest(query.City, query.Country));
        var openWeatherData = await weatherResponse.Content.ReadFromJsonAsync<OpenWeatherMapResponse>(cancellationToken: cancellationToken);

        return new WeatherResponse
        {
            StatusCode = weatherResponse.StatusCode,
            Data = openWeatherData
        };
    }
}