using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using SiteASP.Models;
using SiteASP.Common;

namespace SiteASP.Account
{
    public partial class Login : Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected async void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                ApiBroker api = ApiBroker.prepare();
                bool isValid = await api.ValidateUserAsync(UserName.Text, Password.Text);

                if (isValid)
                {
                    string url = Request.QueryString["ReturnUrl"] ;
                    
                    if(string.IsNullOrEmpty(url)) {
                        url = "~/Account/Welcome.html" ;    
                    }
                    Response.Redirect(url);
                }
                else
                {
                    FailureText.Text = "Invalid username or password.";
                    ErrorMessage.Visible = true;
                }
            }
        }
    }
}