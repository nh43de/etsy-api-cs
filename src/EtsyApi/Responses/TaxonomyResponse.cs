using EtsyApi.Models;

namespace EtsyApi.Responses
{
    public class TaxonomyResponse
    {
        public int count { get; set; }

        public Taxonomy[] results { get; set; }
    }
}
