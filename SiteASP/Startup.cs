using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SiteASP.Startup))]
namespace SiteASP
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
