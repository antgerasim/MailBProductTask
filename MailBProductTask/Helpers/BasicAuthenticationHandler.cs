using MailBProductTask.Domain;
using MailBProductTask.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MailBProductTask.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IClientService _clientService;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, IClientService clientService)
            : base(options, logger, encoder, clock)
        {
            _clientService = clientService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            bool authorize = default(bool);
            long clientId = default(long);
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);

                //Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                //var clientId = credentials;//
                clientId = 12L;//

                authorize = await _clientService.Authenticate(clientId);
            }
            catch
            {
                return AuthenticateResult.Fail("Недопустимый хедер авторизации");
            }

            if (!authorize)
                return AuthenticateResult.Fail("Недопустимый хедер авторизации");

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, clientId.ToString())
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}