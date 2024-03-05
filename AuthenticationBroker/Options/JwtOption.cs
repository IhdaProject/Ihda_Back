namespace AuthenticationBroker.Options;

public class JwtOption
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public double ExpirationInMinutes { get; set; }
    public double ExpirationRefreshTokenInMinutes { get; set; }
}