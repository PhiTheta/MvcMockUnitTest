using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcUnitMockTest.Startup))]
namespace MvcUnitMockTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
