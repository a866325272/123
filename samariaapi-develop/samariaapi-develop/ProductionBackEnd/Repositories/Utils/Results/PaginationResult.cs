using Microsoft.EntityFrameworkCore;
using ProductionBackEnd.Repositories.Utils.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Repositories.Utils.Results
{
    public class PaginationResult<T> where T : class
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="datas">物件</param>
        /// <param name="count">總數</param>
        /// <param name="param">分頁參數</param>
        public PaginationResult(IEnumerable<T> datas, int count, PaginationParam param)
        {
            CurrentPage = param.PageNumber;
            TotalPage = (int)Math.Ceiling(count / (double)param.PageSize);
            PageSize = param.PageSize;
            TotalCount = count;
            Datas = datas;
        }

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Datas { get; set; }
    }
}
