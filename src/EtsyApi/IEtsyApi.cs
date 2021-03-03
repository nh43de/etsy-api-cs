using System;
using System.Threading.Tasks;
using EtsyApi.Models;
using EtsyApi.Responses;
using Refit;

namespace EtsyApi
{
    /*
(.*?)\t(.*?)\t(.*?)\t(.*?)\t(.*?)\r\n
boolean

\ \ \ \ ///\ <summary>\r\n\ \ \ \ ///\ $5\r\n\ \ \ \ ///\ </summary>\r\n\ \ \ \ public $4 $1 { get; set; }\r\n\r\n
bool

     */

    /// <summary>
    /// 
    /// </summary>
    public interface IEtsyApi
    {
        [Get("/listings/active")]
        Task<ListingSearchResult> findAllListingActive(
            [AliasAs("keywords")] string keywords,
            [AliasAs("taxonomy_id")] int? taxonomyId = null,
            [AliasAs("limit")] int? limit = 25, 
            [AliasAs("offset")] int? offset = 0, 
            [AliasAs("page")] int? page = 0
        );

        [Get("/shops/{shop_id}/listings/active")]
        Task<ListingSearchResult> findAllShopListingsActive(
            [AliasAs("shop_id")] string shopId,
            [AliasAs("keywords")] string keywords = null,
            [AliasAs("taxonomy_id")] int? taxonomyId = null,
            [AliasAs("limit")] int? limit = 25,
            [AliasAs("offset")] int? offset = 0,
            [AliasAs("page")] int? page = 0
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listingIds">Comma separated listing ids</param>
        /// <returns></returns>
        [Get("/listings/{listing_id}")]
        Task<ListingSearchResult> getListing(
           [AliasAs("listing_id")] string listingIds, [AliasAs("includes")] string includes = null
        );

        [Get("/taxonomy/buyer/get")]
        Task<TaxonomyResponse> getBuyerTaxonomy();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopIds">Comma separated shop ids</param>
        /// <param name="includes">Included associations.</param>
        /// <returns></returns>
        [Get("/shops/{shop_id}")]
        Task<ShopsResponse> getShops([AliasAs("shop_id")] string shopIds);
    }
}