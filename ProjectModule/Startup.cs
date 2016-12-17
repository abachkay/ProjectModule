using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectModule.Startup))]
namespace ProjectModule
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);            
        }
    }
}
