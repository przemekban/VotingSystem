using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VotingSystem.Startup))]
namespace VotingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
