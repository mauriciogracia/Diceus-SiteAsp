using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using SiteASP.Models;
using SiteASP.Common;

namespace SiteASP.Account
{
    public partial class Register : Page
    {
        protected async void CreateUser_Click(object sender, EventArgs e)
        {
            // Create user
            ApiBroker api = ApiBroker.prepare();
            bool userCreated = await api.CreateUserAsync(UserName.Text, Password.Text);

            if(userCreated)
            {
                string url = "~/Account/Login";
                Response.Redirect(url);
            }
            else
            {
                ErrorMessage.Text = "User could not created, username is taken";
            }
        }
    }
}