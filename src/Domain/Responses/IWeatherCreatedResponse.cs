namespace MinimalBFF.Domain.Responses;

public interface IWeatherCreatedResponse : IWeatherResponse
{
    public string ContentLocation { get; set; }
}