using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LgbtiLibrary.MVC.Startup))]
namespace LgbtiLibrary.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
