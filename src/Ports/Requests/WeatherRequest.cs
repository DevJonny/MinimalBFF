using MinimalBFF.Domain.Responses;
using Paramore.Darker;

namespace MinimalBFF.Ports.Requests;

public class WeatherRequest : IQuery<WeatherResponse>
{
    public string City { get; }
    public string Country { get; }

    public WeatherRequest(string city, string country)
    {
        City = city;
        Country = country;
    }
}