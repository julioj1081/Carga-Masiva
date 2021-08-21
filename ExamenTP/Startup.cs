using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExamenTP.Startup))]
namespace ExamenTP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
