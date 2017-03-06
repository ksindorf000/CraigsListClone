using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CraigsListClone.Startup))]
namespace CraigsListClone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
