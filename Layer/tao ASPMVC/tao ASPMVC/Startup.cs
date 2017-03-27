using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tao_ASPMVC.Startup))]
namespace tao_ASPMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
