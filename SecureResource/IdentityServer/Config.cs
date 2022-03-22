﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client // API
                {
                    ClientId = "movieClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "movieAPI" }
                },
                new Client // MVC App
                {
                    ClientId = "movies_mvc_client",
                    ClientName = "Movies MVC Web App", // Description
                    AllowedGrantTypes = GrantTypes.Code, // Which flow for the client application (STUDY THIS) http://docs.identityserver.io/en/aspnetcore2/topics/grant_types.html
                    RequirePkce = false,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:5002/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:5002/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        //IdentityServerConstants.StandardScopes.Address,
                        //IdentityServerConstants.StandardScopes.Email,
                        "movieAPI",
                        //"roles"
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("movieAPI", "Movie API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
               //new ApiResource("movieAPI", "Movie API")
          };

        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              //new IdentityResources.Address(),
              //new IdentityResources.Email(),
              //new IdentityResource(
              //      "roles",
              //      "Your role(s)",
              //      new List<string>() { "role" })
          };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "fa8fa98f-5f32-47cc-86f9-e90f4d3829e4",
                    Username = "aoshua",
                    Password = "123qwe",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Josh"),
                        new Claim(JwtClaimTypes.FamilyName, "Abbott")
                    }
                }
            };
    }
}
