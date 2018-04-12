using System.Collections.Generic;

namespace MoneyTime.IdentityModel
{
    public class IdentitySettings
    {
        public string IdentityEndPoint { get; set; }
        public IEnumerable<IdentityClient> Clients { get; set; }
        public IEnumerable<IdentityApiResource> ApiResources { get; set; }
    }
}
