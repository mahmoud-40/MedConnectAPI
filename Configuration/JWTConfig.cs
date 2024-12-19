namespace Medical.Configuration;

public class JWTConfig
{
    public required string Key { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public double DurationInMinutes { get; set; }
}
