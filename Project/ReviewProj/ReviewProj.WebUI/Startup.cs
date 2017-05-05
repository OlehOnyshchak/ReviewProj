using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReviewProj.WebUI.Startup))]
namespace ReviewProj.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
