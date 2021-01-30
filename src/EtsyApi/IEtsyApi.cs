﻿using System;
using System.Threading.Tasks;
using EtsyApi.Models;
using EtsyApi.Responses;
using Refit;

namespace EtsyApi
{
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

        [Get("/listings/{listing_id}")]
        Task<ListingSearchResult> getListing(
           [AliasAs("listing_id")] string listingIds
        );

        [Get("/taxonomy/buyer/get")]
        Task<TaxonomyResponse> getBuyerTaxonomy();
    }
}
