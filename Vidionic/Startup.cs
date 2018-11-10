using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vidionic.Startup))]
namespace Vidionic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
