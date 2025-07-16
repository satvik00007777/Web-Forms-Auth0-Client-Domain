using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Auth0DomainAndClientOnly
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["loggedout"] != "true")
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

                /// Manual Redirection for Logout
                authenticationManager.SignOut("Cookies", "Auth0");

                var auth0Domain = ConfigurationManager.AppSettings["Auth0:Domain"];
                var clientId = ConfigurationManager.AppSettings["Auth0:ClientId"];

                // Set returnTo to self with ?loggedout=true to avoid re-logout
                var returnTo = HttpUtility.UrlEncode("https://localhost:44310/Logout.aspx?loggedout=true");

                string logoutUrl = $"https://{auth0Domain}/v2/logout?client_id={clientId}&returnTo={returnTo}";

                Response.Redirect(logoutUrl, false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();

                /// OWIN Automatic Redirection
                //var props = new AuthenticationProperties
                //{
                //    RedirectUri = "https://localhost:44310/Logout.aspx",

                //};
                //authenticationManager.SignOut(
                //    CookieAuthenticationDefaults.AuthenticationType,
                //    OpenIdConnectAuthenticationDefaults.AuthenticationType
                //);

                //Response.Redirect("/login.aspx");
            }
            else
            {
                // ✅ Redirect to Login.aspx after logout completed
                Response.Redirect("~/Default.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
    }
}