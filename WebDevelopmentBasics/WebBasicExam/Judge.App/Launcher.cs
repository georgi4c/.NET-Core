using Microsoft.EntityFrameworkCore;
using Judge.App.Data;
using Judge.App.Infrastructure;
using Judge.App.Infrastructure.Mapping;
using SimpleMvc.Framework;
using SimpleMvc.Framework.Routers;
using System;

namespace Judge.App
{
    class Launcher
    {
        static Launcher()
        {
            using (var db = new JudgeDbContext())
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
