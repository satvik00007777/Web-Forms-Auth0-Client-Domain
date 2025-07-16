using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Owin.Security.Cookies;

namespace Auth0DomainAndClientOnly
{
    public partial class Callback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ctx = HttpContext.Current.GetOwinContext();
            var result = ctx.Authentication.AuthenticateAsync("ExternalCookie").Result;

            if (result?.Identity != null)
            {
                ctx.Authentication.SignIn(result.Identity);
                ctx.Authentication.SignOut("ExternalCookie");
            }

            if(Context.User.Identity.IsAuthenticated) {
                Response.Redirect("~/Authorized.aspx");
            }
        }
    }
}