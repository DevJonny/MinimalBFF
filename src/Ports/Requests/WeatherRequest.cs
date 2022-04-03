using MinimalBFF.Domain.Responses;
using Paramore.Darker;

namespace MinimalBFF.Ports.Requests;

public class WeatherRequest : IQuery<IWeatherResponse>
{
    public float? Lat { get; }
    public float? Lon { get; }

    public WeatherRequest(float? lat, float? lon)
    {
        Lat = lat;
        Lon = lon;
    }
}