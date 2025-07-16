using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;

[assembly: OwinStartup(typeof(Auth0DomainAndClientOnly.Startup))]

namespace Auth0DomainAndClientOnly
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var domain = ConfigurationManager.AppSettings["Auth0:Domain"];
            var clientId = ConfigurationManager.AppSettings["Auth0:ClientId"];
            var redirectUri = ConfigurationManager.AppSettings["Auth0:RedirectUri"];


            app.SetDefaultSignInAsAuthenticationType("Cookies");

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                CookieName = "auth0_cookie",
                AuthenticationMode = AuthenticationMode.Active,
                CookieHttpOnly = true,
                
            });


            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = OpenIdConnectAuthenticationDefaults.AuthenticationType,
                Authority = $"https://{domain}",
                ClientId = clientId,
                RedirectUri = redirectUri,
                ResponseType = "id_token",
                Scope = "openid profile email",
                SignInAsAuthenticationType = "Cookies",
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name"
                },
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    RedirectToIdentityProvider = notification =>
                    {
                        if (notification.ProtocolMessage.RequestType == OpenIdConnectRequestType.Authentication)
                        {
                            // Optional: Customize authentication request (e.g., adding extra parameters)
                            notification.ProtocolMessage.Scope += " offline_access"; // For refresh tokens if needed
                        }
                        else if (notification.ProtocolMessage.RequestType == OpenIdConnectRequestType.Logout)
                        {
                            // Handle logout redirection to Auth0 OIDC logout endpoint
                            var auth0Domain = ConfigurationManager.AppSettings["Auth0:Domain"];
                            var auth0ClientId = ConfigurationManager.AppSettings["Auth0:ClientId"];
                            var auth0PostLogoutRedirectUri = ConfigurationManager.AppSettings["Auth0:PostLogoutRedirectUri"];

                            var logoutUri = $"https://{auth0Domain}/v2/logout?client_id={auth0ClientId}&returnTo={HttpUtility.UrlEncode(auth0PostLogoutRedirectUri)}";

                            notification.Response.Redirect(logoutUri);
                            notification.HandleResponse(); // Suppress the default redirect behavior
                        }

                        return Task.CompletedTask;
                    },

                    AuthorizationCodeReceived = notification =>
                    {
                        // Optional: Handle the authorization code if custom logic is needed
                        return Task.CompletedTask;
                    }
                }

            });
        }
    }
}