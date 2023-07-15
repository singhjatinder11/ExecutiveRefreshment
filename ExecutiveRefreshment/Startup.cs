using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExecutiveRefreshment.Startup))]
namespace ExecutiveRefreshment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
