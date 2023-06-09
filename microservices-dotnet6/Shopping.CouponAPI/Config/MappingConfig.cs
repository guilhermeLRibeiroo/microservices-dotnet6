using AutoMapper;
using Shopping.CouponAPI.Data.ValueObjects;
using Shopping.CouponAPI.Models;

namespace Shopping.CouponAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponVO, Coupon>().ReverseMap();
            });

            return mapper;
        }
    }
}
