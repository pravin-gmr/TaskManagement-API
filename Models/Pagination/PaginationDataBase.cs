namespace API.Models.Pagination
{
    public class PaginationDataBase
    {
        public PaginationDataBase(long pageIndex, long pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public PaginationDataBase(long pageIndex, long pageSize, long totalCount) : this(pageIndex, pageSize)
        {
            TotalCount = totalCount;
        }

        public object? Data { get; set; }
        public long PageIndex { get; set; }
        public long PageSize { get; set; }
        public long TotalCount { get; set; }
    }
}
