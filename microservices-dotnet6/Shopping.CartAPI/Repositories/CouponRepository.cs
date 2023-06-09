using Shopping.CartAPI.Data.ValueObjects;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Shopping.CartAPI.Repositories
{
    public class CouponRepository
        : ICouponRepository
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/Coupon";

        public CouponRepository(HttpClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(client));
        }

        public async Task<CouponVO> GetCouponByCode(string code, string access_token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var response = await _client.GetAsync($"{BasePath}/{code}");

            if (!response.IsSuccessStatusCode)
            {
                return new CouponVO();
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CouponVO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
