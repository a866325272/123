namespace ProductionBackEnd.Models.Products.Enums
{
    /// <summary>
    /// 商品狀態
    /// </summary>
    public enum ProductModelStatus
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
        Publish = 1,
    }
}

