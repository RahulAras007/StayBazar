using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StayBazar.Startup))]
namespace StayBazar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
