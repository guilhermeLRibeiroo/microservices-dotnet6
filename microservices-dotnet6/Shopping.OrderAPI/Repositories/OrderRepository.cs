using Microsoft.EntityFrameworkCore;
using Shopping.OrderAPI.Models;
using Shopping.OrderAPI.Models.Context;

namespace Shopping.OrderAPI.Repositories
{
    public class OrderRepository
        : IOrderRepository
    {
        private readonly DbContextOptions<MySQLContext> _contextOptions;

        public OrderRepository(DbContextOptions<MySQLContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
            if (orderHeader == null) return false;

            await using var _db = new MySQLContext(_contextOptions);
            _db.Headers.Add(orderHeader);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool status)
        {
            await using var _db = new MySQLContext(_contextOptions);
            var header = await _db.Headers.FirstOrDefaultAsync(o => o.Id == orderHeaderId);

            if(header != null)
            {
                header.PaymentStatus = status;
                await _db.SaveChangesAsync();
            }
        }
    }
}
