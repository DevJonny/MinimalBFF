using System.ComponentModel.DataAnnotations;
using MinimalBFF.Domain.Responses;
using MinimalBFF.Ports.Attributes;
using MinimalBFF.Ports.Requests;
using MinimalBFF.Ports.Services;
using Paramore.Darker;

namespace MinimalBFF.Domain.Handlers;

public class GetWeatherHandler : QueryHandlerAsync<WeatherRequest, IWeatherResponse>
{
    private readonly IWeatherService _weatherService;

    public GetWeatherHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [ResultConverter(1)]
    public override async Task<IWeatherResponse> ExecuteAsync(WeatherRequest query, CancellationToken cancellationToken = new())
    {
        var (lon, lat) = (query.Lon, query.Lat);

        if (lon is null || lat is null)
            throw new ValidationException("Lon and Lat must not be null");
        
        var weatherResponse = await _weatherService.GetWeather(new WeatherRequest(lon, lat));

        return weatherResponse;
    }
}