using System.Text.Json.Serialization;

namespace MinimalBFF.Domain.Responses;

public interface IWeatherAcceptedResponse : IWeatherResponse
{
    [JsonIgnore]
    public string Location { get; set; }
}