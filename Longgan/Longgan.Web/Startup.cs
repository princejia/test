using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Longgan.Web.Startup))]
namespace Longgan.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
