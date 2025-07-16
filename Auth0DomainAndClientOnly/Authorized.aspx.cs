using System;

namespace Auth0DomainAndClientOnly
{
    public partial class Authorized : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Protect the page
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            var identity = Context.User.Identity as System.Security.Claims.ClaimsIdentity;
            if (identity != null)
            {
                foreach (var claim in identity.Claims)
                {
                    Response.Write($"<p>{claim.Type} = {claim.Value}</p>");
                }
            }
        }
    }
}
