namespace EtsyApi.Models
{
    public class PaginationDetails
    {
        public int? effective_limit { get; set; }
        public int? effective_offset { get; set; }
        public int? next_offset { get; set; }
        public int? effective_page { get; set; }
        public int? next_page { get; set; }
    }
}
