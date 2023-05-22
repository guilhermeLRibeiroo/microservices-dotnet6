using Shopping.Web.Models;
using Shopping.Web.Services.IServices;
using Shopping.Web.Utils;
using System.Net.Http.Headers;

namespace Shopping.Web.Services
{
    public class CartService
        : ICartService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/Cart";

        public CartService(HttpClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(client));
        }

        public async Task<CartViewModel> FindCartByUserId(string userId, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.GetAsync($"{BasePath}/find-cart/{userId}");
            return await response.ReadContentAs<CartViewModel>();
        }

        public async Task<CartViewModel> AddItemToCart(CartViewModel cart, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.PostAsJson($"{BasePath}/add-cart", cart);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<CartViewModel>();
        }

        public async Task<CartViewModel> UpdateCart(CartViewModel cart, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.PutAsJson($"{BasePath}/update-cart", cart);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<CartViewModel>();
        }

        public async Task<bool> RemoveFromCart(long cartId, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.DeleteAsync($"{BasePath}/remove-cart/{cartId}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<bool>();
        }

        public async Task<bool> ApplyCoupon(CartViewModel cart, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.PostAsJson($"{BasePath}/apply-coupon", cart);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<bool>();
        }

        public async Task<bool> RemoveCoupon(string userId, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.DeleteAsync($"{BasePath}/remove-coupon/{userId}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<bool>();
        }

        public Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearCart(string userId, string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
