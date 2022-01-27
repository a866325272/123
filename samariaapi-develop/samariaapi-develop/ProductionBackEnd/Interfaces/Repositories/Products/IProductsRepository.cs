using Microsoft.AspNetCore.Mvc;
using ProductionBackEnd.Dtos.Products.Results;
using ProductionBackEnd.Models.Products;
using ProductionBackEnd.Repositories.Products.Params;
using ProductionBackEnd.Repositories.Products.Results;
using ProductionBackEnd.Repositories.Utils.Results;
using System.Threading.Tasks;

namespace ProductionBackEnd.Interfaces.Repositories.Products
{
    public interface IProductsRepository
    {
        /// <summary>
        /// 取得商品資訊列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<PaginationResult<ProductsListResult>> GetProductsListAsync(GetProductsListParam param);
        /// <summary>
        /// 取得商品資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductsModel> GetProductsByIdAsync(int id);
        /// <summary>
        /// 新增商品資訊
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task InserProductsAsync(ProductsModel param);
        /// <summary>
        /// 更新商品資訊
        /// </summary>
        /// <param name="param"></param>
        void UpdateProducts(ProductsModel param);
        Task<bool> SaveAllAsync();
    }
}
