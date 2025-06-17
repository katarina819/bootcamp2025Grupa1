namespace Pagination
{
    public class Paginated
    {
        // public List<T> Items { get; set; }   // list of items on current page
        public int TotalCount { get; set; }  // total number of item in full collection
        public int Page { get; set; }        // page number
        public int PageSize { get; set; }    // number of items per page
        
    }

}

