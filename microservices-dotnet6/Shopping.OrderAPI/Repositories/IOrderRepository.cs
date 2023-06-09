using Shopping.OrderAPI.Models;

namespace Shopping.OrderAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader orderHeader);
        Task UpdateOrderPaymentStatus(long orderHeaderId, bool isPaid);
    }
}
