using ProductionBackEnd.Models.PlatformNews.Enums;
using ProductionBackEnd.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Models.PlatformNews
{
    public class PlatformNewsModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 內文
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 照片名稱
        /// </summary>
        public string TitlePageImgName { get; set; }
        /// <summary>
        /// 封面照片
        /// </summary>
        public byte[] TitlePageImg { get; set; }
        /// <summary>
        /// 是否置頂
        /// </summary>
        public bool? IsTop { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        public PlatformNewsModelStatus? Status { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string CreateUserId { get; set; }
        public AppUser CreateUser { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string UpdateUserId { get; set; }
        public AppUser UpdateUser { get; set; }
    }
}
