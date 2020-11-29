namespace EtsyApi.Models
{
    public class ListingSearchResult
    {
        public int count { get; set; }

        public Listing[] results { get; set; }

        public PaginationDetails pagination { get; set; }

    }
}
