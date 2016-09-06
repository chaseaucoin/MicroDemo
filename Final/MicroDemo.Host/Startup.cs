using System.Web.Http;
using Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin;
using Swashbuckle.Application;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;
using MicroDemo.Host.Hubs;
using System;
using System.Threading;

namespace MicroDemo.Host
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            var fileOptions = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = new PhysicalFileSystem(@".\public"),
                RequestPath = PathString.Empty,
            };
            
            fileOptions.DefaultFilesOptions.DefaultFileNames
                = new[] { "index.html" };

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config
                .EnableSwagger(c => c.SingleApiVersion("v1", "Demo API"))
                .EnableSwaggerUi();

            appBuilder.UseWebApi(config);

            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.MapSignalR();

            appBuilder.UseFileServer(fileOptions);

            Task.Factory.StartNew(() => {
                var hub = GlobalHost.ConnectionManager.GetHubContext<IotHub>();
                var rand = new Random();

                while (true)
                {
                    hub.Clients.All.broadcastStat(rand.Next(36));

                    Thread.Sleep(1000);
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
