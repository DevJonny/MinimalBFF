using System.Text.Json.Serialization;

namespace MinimalBFF.Domain.Responses;

public class WeatherResponse : IWeatherResponse
{
    public string Forecast { get; set; }
    public double Temp { get; set; }
    
    [JsonIgnore]
    public IResult Result { get; set; }
}