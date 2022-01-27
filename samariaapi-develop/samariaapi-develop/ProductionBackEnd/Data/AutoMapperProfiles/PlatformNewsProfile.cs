using AutoMapper;
using ProductionBackEnd.Dtos.PlatformNews.Params;
using ProductionBackEnd.Dtos.PlatformNews.Results;
using ProductionBackEnd.Models.PlatformNews;
using ProductionBackEnd.Repositories.PlatformNews.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Data.AutoMapperProfiles
{
    /// <summary>
    /// 平台消息
    /// </summary>
    public class PlatformNewsProfile : Profile
    {
        public PlatformNewsProfile()
        {
            CreateMap<InsertPlatformNewsParam, PlatformNewsModel>();
            CreateMap<PlatformNewsModel, PlatformNewsListResult>();
            CreateMap<PlatformNewsModel, PlatformNewsDetailResult>()
                .ForMember(x => x.CreateUserName, src => src.MapFrom(x => x.CreateUser.RealName))
                .ForMember(x => x.UpdateUserName, src => src.MapFrom(x => x.UpdateUser.RealName));
            CreateMap<UpdatePlatformNewsParam, PlatformNewsModel>();
        }
    }
}
