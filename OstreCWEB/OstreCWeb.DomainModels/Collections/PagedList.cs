namespace OstreCWeb.DomainModels.Collections
{
    public class PagedList<T> where T:class
    {
        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public int ItemCount { get; set; }

        public IList<T> Items { get; set; }
    }
}
