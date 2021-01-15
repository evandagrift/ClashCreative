﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    { }
    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly ICustomAuthenticationManager customAuthenticationManager;

        public CustomAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            ICustomAuthenticationManager customAuthenticationManager) : base(options, logger, encoder, clock)
        {
            this.customAuthenticationManager = customAuthenticationManager;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorized");

            string authorizationHeader = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
                return AuthenticateResult.Fail("Unauthorize");

            if(!authorizationHeader.StartsWith("bearer",StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.Fail("Unauthorize");

            string token = authorizationHeader.Substring("bearer".Length).Trim();

            if(string.IsNullOrEmpty(token))
                return AuthenticateResult.Fail("Unauthorize");

            try
            {
                return ValidateToken(token);
            }
            catch(Exception ex)
            {
                return AuthenticateResult.Fail("Unauthorize");
            }

        }

        private AuthenticateResult ValidateToken(string token)
        {
            var validatedToken = customAuthenticationManager.Tokens.FirstOrDefault(t => t.Key == token);

            if(validatedToken.Key==null)
            {
                return AuthenticateResult.Fail("Unauthorize");
            }

            var claims = new List<Claim>
            {
                //item1 is name
                new Claim(ClaimTypes.Name, validatedToken.Value.Item1),
                new Claim(ClaimTypes.Role, validatedToken.Value.Item2)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);

            //item2 is Role
            var principal = new GenericPrincipal(identity, new[] { validatedToken.Value.Item2});

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);


        }

    }
}
