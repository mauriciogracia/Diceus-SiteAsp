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
        }

        protected async void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                ApiBroker api = ApiBroker.prepare();
                string userName = UserName.Text ;
                bool isValid = await api.ValidateUserAsync(userName, Password.Text);

                if (isValid)
                {
                    string token = await api.StartSessionAsync(userName);
                    string url = "https://sitecore-mgg.azurewebsites.net/Index?t=" + token;
                    
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