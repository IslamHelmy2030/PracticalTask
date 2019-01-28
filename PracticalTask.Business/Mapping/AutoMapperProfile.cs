using AutoMapper;
using PracticalTask.Business.Dto;
using PracticalTask.Data.PracticalDataModel;

namespace PracticalTask.Business.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            UserMappingProfile();
        }

        private void UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserIsActive, opt => opt.MapFrom(src => src.IsActive))
                .ReverseMap();
        }
    }
}
