using Shopping.CartAPI.Data.ValueObjects;

namespace Shopping.CartAPI.Repositories
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCode(string code, string access_token);
    }
}
