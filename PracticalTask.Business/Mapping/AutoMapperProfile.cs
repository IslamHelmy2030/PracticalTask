using AutoMapper;
using PracticalTask.Business.Dto;
using PracticalTask.Business.Dto.Parameter;
using PracticalTask.Data.PracticalDataModel;

namespace PracticalTask.Business.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            UserMappingProfile();
            UsernameParameterMappingProfile();
            UserParameterMappingProfile();
        }

        private void UserMappingProfile()
        {
            CreateMap<User, IUserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserIsActive, opt => opt.MapFrom(src => src.IsActive))
                .ReverseMap();
        }

        private void UserParameterMappingProfile()
        {
            CreateMap<IUserParameterDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username));
        }

        private void UsernameParameterMappingProfile()
        {
            CreateMap<IUsernameParameterDto, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username));
        }
    }
}
