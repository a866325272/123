using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Dtos.PlatformNews.Params
{
    public class UpdatePlatformNewsParam
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
        public UpdatePlatformNewsStatus? Status { get; set; }

        /// <summary>
        /// 最新消息狀態
        /// 
        /// 0：隱藏
        /// 
        /// 1：刊登
        /// </summary>
        public enum UpdatePlatformNewsStatus
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
