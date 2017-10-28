using Microsoft.EntityFrameworkCore;
using ModPanel.App.Data;
using ModPanel.App.Infrastructure;
using ModPanel.App.Infrastructure.Mapping;
using SimpleMvc.Framework;
using SimpleMvc.Framework.Routers;
using System;

namespace ModPanel.App
{
    class Launcher
    {
        static Launcher()
        {
            using (var db = new ModPanelDbContext())
            {
                db.Database.Migrate();
            }

            AutoMapperConfiguration.Initialize();
        }

        public static void Main()
            => MvcEngine.Run(
                new WebServer.WebServer(1337, 
                    DependencyControllerRouter.Get(), 
                    new ResourceRouter()));
    }
}
