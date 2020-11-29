using System.Threading.Tasks;
using EtsyApi.Models;
using Refit;

namespace EtsyApi
{
    public interface IEtsyApi
    {
        [Get("/listings/active")]
        Task<ListingSearchResult> findAllListingActive(
            [AliasAs("keywords")] string keywords,
            [AliasAs("limit")] int? limit = 25, 
            [AliasAs("offset")] int? offset = 0, 
            [AliasAs("page")] int? page = 0
        );
    }
}
