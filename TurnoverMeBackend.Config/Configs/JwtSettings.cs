namespace TurnoverMeBackend.Config.Configs;

public class JwtSettings
{
    public const string Node = "JwtSettings";
    public string SecretKey { get; set; }
    public int ExpiryInMinutes { get; set; }
}