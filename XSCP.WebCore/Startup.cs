using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XSCP.WebCore.Startup))]
namespace XSCP.WebCore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
