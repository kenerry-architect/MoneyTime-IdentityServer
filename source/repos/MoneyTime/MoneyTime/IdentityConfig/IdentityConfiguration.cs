using IdentityServer4.Models;
using MoneyTime.IdentityModel;
using System.Collections.Generic;
using System.Linq;

namespace MoneyTime.IdentityConfig
{
    public class IdentityConfiguration
    {
        public static IEnumerable<ApiResource> GetApiResources(IdentitySettings identitySettings)
        {
            var apiResources = new List<ApiResource>();
            foreach (var item in identitySettings.ApiResources ?? Enumerable.Empty<IdentityApiResource>())
            {
                apiResources.Add(new ApiResource
                {
                    Name = item.Name,
                    DisplayName = item.DisplayName
                });
            }

            return apiResources;
        }

        public static IEnumerable<Client> GetClients(IdentitySettings identitySettings)
        {
            var clients = new List<Client>();
            foreach (var item in identitySettings.Clients ?? Enumerable.Empty<IdentityClient>())
            {
                clients.Add(new Client
                {
                    ClientId = item.ClientId,
                    ClientName = item.ClientName,
                    ClientUri = item.ClientUri,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(item.Secret.Sha256())
                    },
                    AllowedScopes = item.AllowedScopes,
                    AccessTokenLifetime = item.AccessTokenLifetime
                });
            }

            return clients;
        }
    }
}
