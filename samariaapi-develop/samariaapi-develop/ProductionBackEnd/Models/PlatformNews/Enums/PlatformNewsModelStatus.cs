using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Models.PlatformNews.Enums
{
    /// <summary>
    /// 最新消息狀態
    /// </summary>
    public enum PlatformNewsModelStatus
    {
        /// <summary>
        /// 刪除
        /// </summary>
        Deleted = -1,
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
