using ProductionBackEnd.Models.Products.Enums;
using ProductionBackEnd.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Models.Products
{
    public class ProductsModel
    {
        /// <summary>
        /// ID
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
        /// 商品圖片名稱
        /// </summary>
        public string ProductImgName { get; set; }
        /// <summary>
        /// 商品路徑
        /// </summary>
        public byte[] ProductImg { get; set; }
        /// <summary>
        /// 商品是否至頂
        /// </summary>
        public bool? IsTop  { get; set; }
        /// <summary>
        /// 商品狀態
        /// </summary>
        public ProductModelStatus? Status { get; set; }
        public DateTime CreateDateTime { get; set; } 
        public string CreateUserId { get; set; }
        public AppUser CreateUser { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string UpdateUserId { get; set; }
        public AppUser UpdateUser { get; set; }
    }
}
