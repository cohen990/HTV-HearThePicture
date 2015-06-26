using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HearThePicture.Startup))]
namespace HearThePicture
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
