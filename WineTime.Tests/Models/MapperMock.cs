namespace WineTime.Tests.Models
{
    using AutoMapper;
    using Moq;
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });

                return new Mapper(mapperConfig);
            }
        }
    }
}
