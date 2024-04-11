namespace Web.BFF.API.Endpoints.v1.Models.Configuration
{
    public class WebAppConfigurationResponse
    {
        public required string IdentityServerEndpoint { get; set; }
        public required string IdentityServerClientId { get; set; }
    }
}
