using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nathanielhf_comp2084_assignment_1.Startup))]
namespace nathanielhf_comp2084_assignment_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
