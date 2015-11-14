using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HQF.Tutorial.Redis.Web.Startup))]
namespace HQF.Tutorial.Redis.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
