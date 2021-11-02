using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CovidBookingDashboard.Startup))]
namespace CovidBookingDashboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
