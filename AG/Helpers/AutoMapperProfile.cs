using AG.Controllers;
using AG.DTO;
using AG.Models;
using AutoMapper;

namespace AG.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Status, StatusDTO>().ReverseMap();
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<HasDisease, HasDiseaseDTO>().ReverseMap();    
        }
    }
}
