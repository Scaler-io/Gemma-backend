﻿using Gemma.Shared.Constants;
using Gemma.Shared.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace Gemma.Order.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MigrateDatabase<TContext>(this WebApplication app,
            Action<TContext, IServiceProvider> seeder, int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger>();
                var context = services.GetRequiredService<TContext>();

                try
                {
                    logger.Here().Information("Migrating database with context {@DbContextName}", typeof(TContext).Name);

                    InvokeSeeder(seeder, context, services);

                    logger.Here().Information("Migrated database with context {@DbContextName}", typeof(TContext).Name);
                }catch (SqlException ex)
                {
                    logger.Here().Error("{@ErrorCode} Migration failed. {@Message} - {@StackTrace}", ErrorCodes.Operationfailed, ex.Message, ex.StackTrace);
                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(app, seeder, retryForAvailability);
                    }
                }

            }
            return app;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
