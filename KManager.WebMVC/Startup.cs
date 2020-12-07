using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KManager.WebMVC.Startup))]
namespace KManager.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
