namespace WineTime
{
    using AutoMapper;
    using WineTime.Areas.Admin.Models;
    using WineTime.Core.Models;
    using WineTime.Infrastructure.Data;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductsServiceModel, ProductFormModel>();
            CreateMap<Product, ProductListingViewModel>();
            CreateMap<Product, ProductsServiceModel>();
            CreateMap<Region, ProductRegionServiceModel>();
            CreateMap<Category, ProductCategoryServiceModel>();
            CreateMap<Manufacture, ProductManufactureServiceModel>()
                .ForMember(x => x.Region, cfg => cfg.MapFrom(r => r.Region.Country));

        }
    }
}
