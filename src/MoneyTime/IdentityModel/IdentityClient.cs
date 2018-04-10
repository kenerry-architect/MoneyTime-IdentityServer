namespace MoneyTime.IdentityModel
{
    public class IdentityClient
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public string Secret { get; set; }
        public string[] AllowedScopes { get; set; }
        public int AccessTokenLifetime { get; set; }
    }
}
