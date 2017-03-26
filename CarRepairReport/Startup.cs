using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarRepairReport.Startup))]
namespace CarRepairReport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
