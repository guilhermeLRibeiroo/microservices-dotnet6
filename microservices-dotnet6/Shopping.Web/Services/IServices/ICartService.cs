using Shopping.Web.Models;

namespace Shopping.Web.Services.IServices
{
    public interface ICartService
    {
        Task<CartViewModel> FindCartByUserId(string userId, string accessToken);
        Task<CartViewModel> AddItemToCart(CartViewModel cart, string accessToken);
        Task<CartViewModel> UpdateCart(CartViewModel cart, string accessToken);
        Task<bool> RemoveFromCart(long cartDetailId, string accessToken);
        Task<bool> ApplyCoupon(CartViewModel cart, string accessToken);
        Task<bool> RemoveCoupon(string userId, string accessToken);
        Task<object> Checkout(CartHeaderViewModel cartHeader, string accessToken);
    }
}
