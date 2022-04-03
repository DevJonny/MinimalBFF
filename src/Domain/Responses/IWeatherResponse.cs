namespace MinimalBFF.Domain.Responses;

public interface IWeatherResponse
{
    public string Forecast { get; set; }
    public double Temp { get; set; }
    IResult Result { get; set; }
}