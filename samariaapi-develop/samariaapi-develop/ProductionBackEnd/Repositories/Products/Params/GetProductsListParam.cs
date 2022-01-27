using ProductionBackEnd.Repositories.Utils.Params;

namespace ProductionBackEnd.Repositories.Products.Params
{
    public class GetProductsListParam : PaginationParam
    {
        /// <summary>
        /// 商品名稱
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string ProductDescription { get; set; }
    }
}
