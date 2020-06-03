using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApi
{
    public static class MigrateDatabaseExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost webHost) where T:DbContext
        {
            using(var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<T>();
                    Thread.Sleep(5000); // hacky, don't do this
                    db.Database.Migrate();
                } catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured trying to migrate the database");
                }
            }

            return webHost;
        }
    }
}
