using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Falcon_IssueTracker.Startup))]
namespace Falcon_IssueTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
