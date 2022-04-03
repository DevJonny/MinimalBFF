namespace MinimalBFF.Ports;

public record Config(WeatherConfig WeatherConfig);
public record WeatherConfig (string ApiKey);