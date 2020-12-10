using AutoMapper;
using ProductCatalog.Entities;
using ProductCatalog.WebApi.DTOs;

namespace ProductCatalog.WebApi.Mapping
{
    public class OrganizationProfile : Profile
	{
		public OrganizationProfile()
		{
			CreateMap<Product, ProductDto>().ReverseMap();
		}
	}
}
