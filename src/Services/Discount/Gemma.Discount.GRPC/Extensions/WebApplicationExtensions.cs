using Gemma.Shared.Constants.Database;
using Gemma.Shared.Extensions;
using Npgsql;
using ILogger = Serilog.ILogger;

namespace Gemma.Discount.GRPC.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MigrateDatabase(this WebApplication app, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger>();

                try
                {
                    logger.Here().Information("Migrating discount database.");
                    using var connection = new NpgsqlConnection(
                        configuration["DiscountDb:ConnectionString"]
                    );
                    connection.Open();
                    var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };
                    // drop table coupon if exists
                    command.CommandText = DiscountDbCommands.DropIfExist;
                    command.ExecuteNonQuery();

                    // create table coupon
                    command.CommandText = DiscountDbCommands.CreateCouponTable;
                    command.ExecuteNonQuery();

                    // insert data into coupon
                    command.CommandText = $"INSERT INTO Coupon(ProductId, ProductName, Description, Amount, CreatedAt, UpdatedAt) VALUES('602d2149e773f2a3990b47f5', 'IPhone X', 'IPhone Discount 1', 150, '{DateTime.Now}', '{DateTime.Now}');";
                    command.ExecuteNonQuery();

                    command.CommandText = $"INSERT INTO Coupon(ProductId, ProductName, Description, Amount, CreatedAt, UpdatedAt) VALUES('602d2149e773f2a3990b47f6', 'Samsung 10', 'Samsung Discount', 100, '{DateTime.Now}', '{DateTime.Now}');";
                    command.ExecuteNonQuery();

                    logger.Here().Information("Migrated discount database.");

                }
                catch (NpgsqlException e)
                {
                    logger.Error(e, "An error occured while migrating discount database.");
                    if (retryForAvailability < 10)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase(app, retryForAvailability);
                    }
                }
            }

            return app;
        }
    }
}
