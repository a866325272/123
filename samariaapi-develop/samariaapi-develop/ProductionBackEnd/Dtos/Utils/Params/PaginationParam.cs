using System;
using System.ComponentModel.DataAnnotations;

namespace ProductionBackEnd.Dtos.Utils.Params
{
    public class PaginationParam
    {
        private int maxPageSize = 50;
        private int pageSize = 10;

        /// <summary>
        /// 分頁
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// 每頁筆數
        /// </summary>
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
