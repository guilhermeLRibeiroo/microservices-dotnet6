using Shopping.Web.Models;
using Shopping.Web.Services.IServices;
using Shopping.Web.Utils;
using System.Net.Http.Headers;

namespace Shopping.Web.Services
{
    public class CouponService
       : ICouponService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/Coupon";

        public CouponService(HttpClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(client));
        }

        public async Task<CouponViewModel> GetCoupon(string code, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.GetAsync($"{BasePath}/{code}");

            if (!response.IsSuccessStatusCode)
            {
                return new CouponViewModel();
            }

            return await response.ReadContentAs<CouponViewModel>();
        }
    }
}
