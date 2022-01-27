using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProductionBackEnd.Dtos.Products.Params
{
    /// <summary>
    /// 新增商品參數定義
    /// </summary>
    public class InsertProductsParam
    {
        /// <summary>
        /// 商品名稱
        /// </summary>
        [Required]
        public string ProductName { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        [Required]
        public string ProductDescription { get; set; }
        /// <summary>
        /// 商品照片
        /// </summary>
        public IFormFile ProductImgFile { get; set; }
        /// <summary>
        /// 是否置頂
        /// </summary>
        [Required]
        public bool? IsTop  { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        [Required]
        public InsertProductStatus? Status { get; set; }
        /// <summary>
        /// 商品狀態
        /// </summary>
        public enum InsertProductStatus
        {
            /// <summary>
            /// 隱藏
            /// </summary>
            Disable = 0,
            /// <summary>
            /// 刊登
            /// </summary>
            Publish = 1,
        }
    }
}
