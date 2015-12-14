using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LawyersAdda.Startup))]
namespace LawyersAdda
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
