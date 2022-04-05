namespace WineTime
{
    using AutoMapper;
    using WineTime.Areas.Admin.Models;
    using WineTime.Core.Models;
    using WineTime.Core.Models.Degustations;
    using WineTime.Infrastructure.Data;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductsServiceModel, ProductFormModel>().ReverseMap();
            CreateMap<Product, ProductListingViewModel>()
                .ForMember(x => x.Category, cfg => cfg.MapFrom(c => c.Category.Name));
            CreateMap<Product, ProductsServiceModel>();
            CreateMap<Region, ProductRegionServiceModel>();
            CreateMap<Category, ProductCategoryServiceModel>();
            CreateMap<Manufacture, ProductManufactureServiceModel>()
                .ForMember(x => x.Region, cfg => cfg.MapFrom(r => r.Region.Country));
            CreateMap<DegustationsServiceViewModel, DegustationsFormModel>();
            CreateMap<Degustation, DegustationsServiceViewModel>()
                .ForMember(x=> x.DateTime,cfg => cfg.MapFrom(d => d.DateTime.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(x => x.BookSeats, cfg => cfg.MapFrom(d => d.Users.Count));

        }
    }
}
