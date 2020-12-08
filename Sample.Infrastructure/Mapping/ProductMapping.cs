using AutoMapper;
using System.Linq;
using Sample.Core.Entities;
using Sample.Core.UseCases;
using Sample.SharedKernal;

namespace Sample.Infrastructure.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductAddInputDto, Product>().ReverseMap();

            CreateMap<ProductAddInputDto, ProductUpdateInputDto>().ReverseMap();

            CreateMap<ProductUpdateInputDto, Product>()
                .ForMember(d => d.Photo, o => o.Ignore()).ReverseMap();
            


            CreateMap<Product, ProductGetByIdOutputDto>();

            CreateMap<Product, ProductGetAllOutputDto>();

            CreateMap<Product, IdNameDto>().ReverseMap();
        }



    }
}
