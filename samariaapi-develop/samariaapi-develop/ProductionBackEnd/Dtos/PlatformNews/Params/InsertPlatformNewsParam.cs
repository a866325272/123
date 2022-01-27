using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProductionBackEnd.Dtos.PlatformNews.Params
{
    public class InsertPlatformNewsParam
    {
        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        /// <summary>
        /// 內文
        /// </summary>
        [Required]
        public string Content { get; set; }
        /// <summary>
        /// 封面照片
        /// </summary>
        public IFormFile TitlePageImgFile { get; set; }
        /// <summary>
        /// 是否置頂
        /// </summary>
        [Required]
        public bool? IsTop { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        [Required]
        public InsertPlatformNewsStatus? Status { get; set; }

        /// <summary>
        /// 最新消息狀態
        /// 
        /// 0：隱藏
        /// 
        /// 1：刊登
        /// </summary>
        public enum InsertPlatformNewsStatus
        {
            /// <summary>
            /// 隱藏
            /// </summary>
            Disable = 0,
            /// <summary>
            /// 刊登
            /// </summary>
            Publish = 1
        }
    }
}
