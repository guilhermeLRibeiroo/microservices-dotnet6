using AutoMapper;
using Shopping.CartAPI.Data.ValueObjects;
using Shopping.CartAPI.Models;

namespace Shopping.CartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductVO, Product>().ReverseMap();
                config.CreateMap<CartHeaderVO, CartHeader>().ReverseMap();
                config.CreateMap<CartDetailVO, CartDetail>().ReverseMap();
                config.CreateMap<CartVO, Cart>().ReverseMap();
            });

            return mapper;
        }
    }
}
