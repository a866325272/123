using AutoMapper;
using ProductionBackEnd.Dtos.User.Params;
using ProductionBackEnd.Dtos.User.Results;
using ProductionBackEnd.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Data.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            // 使用者
            CreateMap<RegisterParam, AppUser>();
            CreateMap<AppUser, AccountDetailResult>();
        }
    }
}
