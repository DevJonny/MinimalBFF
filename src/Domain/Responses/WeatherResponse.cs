using System.Net;

namespace MinimalBFF.Domain.Responses;

public class WeatherResponse : IWeatherResponse
{
    public HttpStatusCode StatusCode { get; init; }
    public object Data { get; init; }
    public IResult Result { get; set; }
}