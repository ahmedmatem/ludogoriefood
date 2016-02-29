using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LudogorieFood.Web.Startup))]
namespace LudogorieFood.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
