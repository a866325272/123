using AutoMapper;
using ProductionBackEnd.Dtos.Products.Params;
using ProductionBackEnd.Dtos.Products.Results;
using ProductionBackEnd.Models.Products;
using ProductionBackEnd.Repositories.Products.Results;

namespace ProductionBackEnd.Data.AutoMapperProfiles
{
    /// <summary>
    /// 商品資訊
    /// </summary>
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<InsertProductsParam, ProductsModel>();
            CreateMap<ProductsModel, ProductsListResult>();
            CreateMap<ProductsModel, ProductsDetailResult>()
                .ForMember(x => x.CreateUserName, src => src.MapFrom(x => x.CreateUser.RealName))
                .ForMember(x => x.UpdateUserName, src => src.MapFrom(x => x.UpdateUser.RealName));
            CreateMap<UpdateProductsParam, ProductsModel>();
        }
    }
}
