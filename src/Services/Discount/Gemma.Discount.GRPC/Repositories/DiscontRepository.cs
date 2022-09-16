using Dapper;
using Gemma.Discount.GRPC.Entities;
using Gemma.Shared.Constants.Database;
using Gemma.Shared.Extensions;
using Npgsql;
using ILogger = Serilog.ILogger;

namespace Gemma.Discount.GRPC.Repositories
{
    public class DiscontRepository: IDiscountRepository
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly NpgsqlConnection _connection;

        public DiscontRepository(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connection = EstablishConnection();
        }

        public async Task<IEnumerable<Coupon>> GetAllCoupons()
        {
            var command = new CommandDefinition(DiscountDbCommands.SelectAll);
            var coupons = await _connection.QueryAsync<Coupon>(command);
            return coupons;
        }

        public async Task<Coupon> GetCoupon(string id)
        {
            var command = new CommandDefinition(DiscountDbCommands.SelectByProductId, new { ProductId = id });
            var coupon = await _connection.QueryFirstOrDefaultAsync<Coupon>(command);
            _logger.Here().MethodExited();
            return coupon;
        }

        public async Task<Coupon> GetCouponByProductName(string productName)
        {
            var command = new CommandDefinition(DiscountDbCommands.SelectByProductName, new { ProductName = productName });
            var coupon = await _connection.QueryFirstOrDefaultAsync<Coupon>(command);
            _logger.Here().MethodExited();
            return coupon;
        }

        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            var affected = 0;
            var command = new CommandDefinition(DiscountDbCommands.Insert, new
            {
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount,
                CreatedAt = coupon.CreatedAt,
                UpdatedAt = coupon.UpdatedAt
            });
            affected = await _connection.ExecuteAsync(command);
            _logger.Here().MethodExited();
            return affected > 0;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            var affected = 0;
            var command = new CommandDefinition(DiscountDbCommands.Update, new
            {
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount,
                UpdatedAt = coupon.UpdatedAt,
                Id = coupon.Id
            });
            affected = await _connection.ExecuteAsync(command);
            _logger.Here().MethodExited();
            return affected > 0;
        }

        public async Task<bool> DeleteCoupon(int id)
        {
            var affected = 0;
            var command = new CommandDefinition(DiscountDbCommands.Delete, new { Id = id });
            affected = await _connection.ExecuteAsync(command);
            _logger.Here().MethodExited();
            return affected > 0;
        }

        public void Dispose()
        {
            try
            {
                _connection.Close();
            }
            catch (Exception e)
            {
                _logger.Error("Unable to close connection", e);
            }
        }

        private NpgsqlConnection EstablishConnection()
        {
            var connection = new NpgsqlConnection(
                    _configuration["DiscountDb:ConnectionString"]
                );
            _logger.Here().Information("Connection establised to DiscountDb");
            return connection;
        }
    }
}
