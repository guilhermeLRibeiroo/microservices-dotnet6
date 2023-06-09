using Shopping.Web.Models;

namespace Shopping.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCoupon(string code, string accessToken);
    }
}
