using AutoMapper;
using Ewenze.Application.Features.UserFeature.Commands.CreateUser;
using Ewenze.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Features.UserFeature.Dto
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<CreateUserCommand, User>()
                .ForMember(u => u.NiceName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(u => u.DisplayName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(u => u.LoginName, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
