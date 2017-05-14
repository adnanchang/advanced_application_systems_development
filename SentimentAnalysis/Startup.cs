using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SentimentAnalysis.Startup))]
namespace SentimentAnalysis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
