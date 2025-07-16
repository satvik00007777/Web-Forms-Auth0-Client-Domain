using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Web;

namespace Auth0DomainAndClientOnly
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (IsValidEmail(email))
            {
                var authProps = new AuthenticationProperties
                {
                    RedirectUri = "/Authorized.aspx"
                };

                authProps.Dictionary["login_hint"] = email;

                HttpContext.Current.GetOwinContext().Authentication.Challenge(
                    authProps,
                    OpenIdConnectAuthenticationDefaults.AuthenticationType
                );

                Response.StatusCode = 401; // required to trigger challenge
                Response.End();
            }
            else
            {
                lblMessage.Text = "Invalid email. Please enter a valid address.";
            }
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }

        //private bool IsValidEmail(string email)
        //{
        //    // ✅ Example: allow only company emails
        //    return email.EndsWith("@yourcompany.com", StringComparison.OrdinalIgnoreCase);
        //}
    }
}
