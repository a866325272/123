using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Dtos.PlatformNews.Results
{
    public class PlatformNewsDetailResult
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
        /// 是否置頂
        /// </summary>
        public bool? IsTop { get; set; }
        /// <summary>
        /// 狀態
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
