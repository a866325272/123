namespace ProductionBackEnd.Repositories.Utils.Params
{
    /// <summary>
    /// 分頁參數
    /// </summary>
    public class PaginationParam
    {
        private int maxPageSize = 50;
        private int pageSize = 10;
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
