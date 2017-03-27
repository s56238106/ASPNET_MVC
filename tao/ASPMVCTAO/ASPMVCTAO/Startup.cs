using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPMVCTAO.Startup))]
namespace ASPMVCTAO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
