using System.Text.Json.Serialization;

namespace MinimalBFF.Adapters.OpenWeatherMap;

public record OpenWeatherMapResponse
(
    Coord Coord, 
    Weather Weather,
    string Base,
    Main Main,
    int Visibility,
    Wind Wind,
    Clouds Clouds,
    int Dt,
    Sys Sys,
    int Timezone,
    int Id,
    string Name,
    int Cod
);

public record Coord(double Lon, double Lat);
public record Weather(int Id, string Main, string Description, string Icon);

public record Main(double Temp, double FeelsLike, double TempMin, double TempMax, int Pressure, int Humidity)
{
    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; init; }
    
    [JsonPropertyName("temp_min")] 
    public double TempMin { get; init; }
    
    [JsonPropertyName("temp_max")] 
    public double TempMax { get; init; }
}

public record Wind (double Speed, int Degrees)
{
    [JsonPropertyName("deg")]
    public int Degrees { get; init; }
}

public record Clouds(int All);

public record Sys(int Type, int Id, double Message, string Country, int Sunrise, int Sunset);