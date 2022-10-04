namespace Gateways.Services.Common.Sieve.Pagination
{
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> Results { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public long RowCount { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
