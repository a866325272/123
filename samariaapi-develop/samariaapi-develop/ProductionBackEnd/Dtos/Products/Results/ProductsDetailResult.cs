using System;

namespace ProductionBackEnd.Dtos.Products.Results
{
    public class ProductsDetailResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商品名稱
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string ProductDescription { get; set; }
        /// <summary>
        /// 是否置頂
        /// </summary>
        public bool? IsTop { get; set; }
        /// <summary>
        /// 商品狀態
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 創建時間
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 創建人員
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime UpdateDateTime { get; set; }
        /// <summary>
        /// 更新人員
        /// </summary>
        public string UpdateUserName { get; set; }
    }
}
