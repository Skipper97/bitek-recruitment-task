using AutoMapper;

namespace RecruitmentTask.Api.MappingProfiles
{
    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile() 
        {
            CreateMap<Models.Product, Data.Entities.Product>().ReverseMap();
            CreateMap<Models.Inventory, Data.Entities.Inventory>().ReverseMap();
            CreateMap<Models.Price, Data.Entities.Price>().ReverseMap();
        }
    }
}
