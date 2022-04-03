using System.Text.Json.Serialization;

namespace MinimalBFF.Adapters.OpenWeatherMap;

public class OpenWeatherMapResponse
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string Timezone { get; set; }
    [JsonPropertyName("timezone_offset")]
    public int TimezoneOffset { get; set; }
    [JsonPropertyName("current")]
    public Weather CurrentWeather { get; set; }
}

public class Weather
{
    public int Dt { get; set; }
    public int Sunrise { get; set; }
    public int Sunset { get; set; }
    public double Temp { get; set; }
    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    [JsonPropertyName("dew_point")]
    public double DewPoint { get; set; }
    public double Uvi { get; set; }
    public int Clouds { get; set; }
    public int Visibility { get; set; }
    [JsonPropertyName("wind_speed")]
    public double WindSpeed { get; set; }
    [JsonPropertyName("wind_deg")]
    public int WindDeg { get; set; }
    [JsonPropertyName("wind_gust")]
    public double WindGust { get; set; }
    [JsonPropertyName("weather")]
    public List<WeatherDescription> WeatherDescription { get; set; }
}

public class WeatherDescription
    {
    public int Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}