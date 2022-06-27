using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lottery365.DTO;
using Lottery365.Models;

namespace Lottery365.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>().ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(x => x.Role.Name)).ForMember(x=>x.Password,opt=>opt.Ignore());
            CreateMap<WheelAndWinnerDetailsDTO, WheelInfo>();
            CreateMap<WheelInfo, WheelAndWinnerDetailsDTO>();
            CreateMap<UserLotteryDetail, UserLotteryDetailsDTO>();
            CreateMap<UserLotteryDetailsDTO, UserLotteryDetail>();
            CreateMap<WheelInfoDTO, WheelInfo>();
        }
    }
}
