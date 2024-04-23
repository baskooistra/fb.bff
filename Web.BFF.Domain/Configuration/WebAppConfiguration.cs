namespace Web.BFF.Domain.Configuration;

public class WebAppConfiguration
{
    public const string Key = "Configuration";

    public required string[] FrontendDomains { get; set; }
    public required string IdentityServerEndpoint { get; set; }
    public required string IdentityServerClientId { get; set; }
}
