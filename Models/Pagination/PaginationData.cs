namespace API.Models.Pagination
{
    public class PaginationData<T> : PaginationDataBase
    {
        public delegate R PaginationDataAction<out R>(T obj);
        public delegate object PaginationDataAction(T obj);

        public PaginationData(long pageIndex, long pageSize) : base(pageIndex, pageSize) { }
        public PaginationData(long pageIndex, long pageSize, long totalCount) : base(pageIndex, pageSize, totalCount) { }

        public new List<T>? Data { get; set; }

        public PaginationDataBase ToResponseModel(PaginationDataAction wrapper)
        {
            var paginationData = new PaginationData<object>(PageIndex, PageSize, TotalCount)
            {
                Data = new List<object>()
            };

            if (wrapper != null && Data != null && Data.Any())
            {
                foreach (var data in Data)
                {
                    var model = wrapper.Invoke(data);
                    paginationData.Data.Add(model);
                }
            }

            return paginationData;
        }

        public virtual PaginationData<R> ToResponseModel<R>(PaginationDataAction<R> wrapper)
        {
            var paginationData = new PaginationData<R>(PageIndex, PageSize, TotalCount)
            {
                Data = new List<R>()
            };

            if (wrapper != null && Data != null && Data.Any())
            {
                foreach (var data in Data)
                {
                    var model = wrapper.Invoke(data);
                    paginationData.Data.Add(model);
                }
            }

            return paginationData;
        }
    }
}
