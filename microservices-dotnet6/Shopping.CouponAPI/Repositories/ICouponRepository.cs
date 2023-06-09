using Shopping.CouponAPI.Data.ValueObjects;

namespace Shopping.CouponAPI.Repositories
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCode(string code);
    }
}
