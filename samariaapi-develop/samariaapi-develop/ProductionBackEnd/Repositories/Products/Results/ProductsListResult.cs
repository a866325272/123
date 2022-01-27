using System;

namespace ProductionBackEnd.Repositories.Products.Results
{
    public class ProductsListResult
    {
        /// <summary>
        /// 資料庫商品ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商品名稱
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 商品狀態
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime UpdateDateTime { get; set; }
    }
}
