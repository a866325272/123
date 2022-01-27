using AutoMapper;
using ProductionBackEnd.Models.User;
using ProductionBackEnd.Repositories.Admin.Results;
using System.Linq;

namespace ProductionBackEnd.Data.AutoMapperProfiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            // 管理者
            CreateMap<AppUser, AdminAccountListResult>();
            CreateMap<AppUser, AdminAccountDetailResult>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role.Name)));
        }
    }
}
