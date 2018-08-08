using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LogsParser.Web.Startup))]

namespace LogsParser.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}