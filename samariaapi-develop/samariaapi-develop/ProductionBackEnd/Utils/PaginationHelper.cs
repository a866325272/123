using Microsoft.EntityFrameworkCore;
using ProductionBackEnd.Repositories.Utils.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Utils
{
    public static class PaginationHelper
    {

        /// <summary>
        /// 分頁
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async static Task<IEnumerable<T>> PaginationData<T>(this IQueryable<T> source, PaginationParam param)
        {
            return await source.Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync();
        }
    }
}
