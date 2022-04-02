using System.Net;

namespace MinimalBFF.Domain.Responses;

public interface IWeatherResponse
{
    IResult Result { get; set; }
    HttpStatusCode StatusCode { get; init; }
    object Data { get; init; }
}