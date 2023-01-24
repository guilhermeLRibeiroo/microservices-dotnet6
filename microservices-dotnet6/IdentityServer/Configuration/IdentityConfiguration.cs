using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer.Configuration
{
    public static class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Client = "Client";

        public static IEnumerable<IdentityResource> IdentityResources
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes
            => new List<ApiScope>
            {
                new ApiScope("shopping", "Shopping Server"),
                new ApiScope(name: "read", "Read Data"),
                new ApiScope(name: "write", "Write Data"),
                new ApiScope(name: "delete", "Delete Data"),
            };

        public static IEnumerable<Client> Clients
            => new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets =
                    {
                        new Secret("this_secret_should_be_at_appsettings".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "read",
                        "write",
                        "profile"
                    }
                },
                new Client
                {
                    ClientId = "shopping",
                    ClientSecrets =
                    {
                        new Secret("this_secret_should_be_at_appsettings".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris =
                    {
                        "https://localhost:52070/signin-oidc"
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:52070/signout-callback-oidc"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "shopping"
                    }
                }
            };
    }
}
