namespace WebAPIServices.Helper
{
    public class QueryObject
    {
        public string? CategoryName { get; set; } = null;
        public string? ProductName { get; set; } = null;
        public double? PriceProduct { get; set; }
        public string? SortBy { get; set; } = null ;
        public bool isDecsending { get; set; } = false ;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
