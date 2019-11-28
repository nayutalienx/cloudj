
using IdentityServer4;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace IdentityServer.Controllers
{
    public static class Configuration
    {
        /// <summary>
        /// Clients IS
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityServer4.EntityFramework.Entities.Client> GetClients() {
            return new List<IdentityServer4.EntityFramework.Entities.Client> {
                new IdentityServer4.Models.Client
                {
                    ClientId = "cloudj-app",
                    ClientSecrets = { new IdentityServer4.Models.Secret("cloudj-app".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    AbsoluteRefreshTokenLifetime = default,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequireConsent = false,
                    RedirectUris = { "http://localhost:63302/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:63302/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "cloudj-api"
                    },

                    AllowOfflineAccess = true
                }.ToEntity()
            };
        }

        /// <summary>
        /// Identity resources
        /// </summary>
        /// <returns></returns>
        public static List<IdentityServer4.EntityFramework.Entities.IdentityResource> GetIdentityResources()
        {
            return new List<IdentityServer4.EntityFramework.Entities.IdentityResource> 
            {
                new IdentityResources.OpenId().ToEntity(),
                new IdentityResources.Profile().ToEntity(),
                new IdentityResources.Email().ToEntity(),
                new IdentityServer4.Models.IdentityResource { Name = "role" , UserClaims = new List<string> {"role" } }.ToEntity()

            };
        }

        /// <summary>
        /// Api resources
        /// </summary>
        /// <returns></returns>
        public static List<IdentityServer4.EntityFramework.Entities.ApiResource> GetApiResources() 
        {
            return new List<IdentityServer4.EntityFramework.Entities.ApiResource>
            {
                new IdentityServer4.Models.ApiResource
                {
                        Name = "cloudj-api",
                        DisplayName = "CloudJ Api",
                        ApiSecrets = { new IdentityServer4.Models.Secret("cloudj-api".Sha256()) },
                        Scopes = { new Scope("cloudj-api", "CloudJ Api") },
                        UserClaims = { "role" }
                }.ToEntity()
            };
        }

        /// <summary>
        /// Identity users
        /// </summary>
        /// <returns></returns>
        public static List<IdentityUser> GetIdentityUsers() {
            return new List<IdentityUser>
            {
                new IdentityUser
                {
                    Email = "alice@alice.com",
                    UserName = "alice@alice.com"
                },

                new IdentityUser
                {
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com"
                }
            };
        }

        /// <summary>
        /// Identity roles
        /// </summary>
        /// <returns></returns>
        public static List<IdentityRole> GetIdentityRoles() {
            return new List<IdentityRole> 
            {
                new IdentityRole
                {
                    Name = "Admin"
                },

                new IdentityRole
                {
                    Name = "User"
                }
            };
        }

        /// <summary>
        /// Add roles to users
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetUsernamesToRoles() {
            return new Dictionary<string, string>
            {
                { "alice@alice.com", "User" },
                { "admin@admin.com", "Admin" }
            };
        }
    }
}
