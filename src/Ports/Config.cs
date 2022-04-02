namespace MinimalBFF.Ports;

public record Config(Weather Weather);
public record Weather (string ApiKey);