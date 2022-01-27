using ProductionBackEnd.Dtos.Utils.Params;

namespace ProductionBackEnd.Dtos.Products.Params
{
    /// <summary>
    /// 取得商品列表參數定義
    /// </summary>
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
