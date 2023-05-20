using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.CouponAPI.Data.ValueObjects;
using Shopping.CouponAPI.Repositories;

namespace Shopping.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CouponController 
        : ControllerBase
    {
        private ICouponRepository _couponRepository;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        }

        [HttpGet("{code}")]
        [Authorize]
        public async Task<ActionResult<CouponVO>> GetCouponByCode(string code)
        {
            var coupon = await _couponRepository.GetCouponByCode(code);
            if (coupon == null) return NotFound();
            return Ok(coupon);
        }
    }
}