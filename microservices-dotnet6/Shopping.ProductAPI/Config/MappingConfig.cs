using AutoMapper;
using Shopping.ProductAPI.Models;
using Shopping.ProductAPI.ValueObjects;

namespace Shopping.ProductAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductVO, Product>();
                config.CreateMap<Product, ProductVO>();
            });

            return mapper;
        }
    }
}
