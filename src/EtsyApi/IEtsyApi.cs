using System;
using System.Threading.Tasks;
using EtsyApi.Models;
using EtsyApi.Responses;
using Refit;

namespace EtsyApi
{
    /*

>>> Generator for etsy doc fields table to cs class

    Find

(.*?)\t(.*?)\t(.*?)\t(.*?)\r\n
boolean
array\((.*)\)

    Replace 

\ \ \ \ ///\ <summary>\r\n\ \ \ \ ///\ $5\r\n\ \ \ \ ///\ </summary>\r\n\ \ \ \ public $4 $1 { get; set; }\r\n\r\n
bool
$1[]

>>> For enums

    Find

enum\((.*)\)
,
    
    -- when needing [EnumMember] attributes add: 
    
 ?,
\t(.*),
    
    Replace

public enum ENUM_NAME { \r\n\t$1\r\n}
,\r\n\t
    
    -- when needing [EnumMember] attributes add: 

,\r\n\t
\t[EnumMember(Value = "$1")]\r\n\t_$1,


>>>> Generator for etsy fields to params for etsy api

(.*?)\t(.*?)\t(.*?)\t(.*?)\r\n
boolean
enum\(.*\)
array\((.*)\)

[AliasAs("$1")] $4 $1,\r\n
bool
ENUM_NAME
$1[]

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
            [AliasAs("page")] int? page = 0,
            [AliasAs("includes")] string includes = null
        );

        [Get("/shops/{shop_id}/listings/active")]
        Task<ListingSearchResult> findAllShopListingsActive(
            [AliasAs("shop_id")] string shopId,
            [AliasAs("keywords")] string keywords = null,
            [AliasAs("taxonomy_id")] int? taxonomyId = null,
            [AliasAs("limit")] int? limit = 25,
            [AliasAs("offset")] int? offset = 0,
            [AliasAs("page")] int? page = 0, 
            [AliasAs("includes")] string includes = null
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listingIds">Comma separated listing ids</param>
        /// <param name="includes"></param>
        /// <returns></returns>
        [Get("/listings/{listing_id}")]
        Task<ListingSearchResult> getListing(
           [AliasAs("listing_id")] string listingIds,
           [AliasAs("includes")] string includes = null
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Get("/users/{user_id}/shipping/templates")]
        Task<GenericResponse<ShippingTemplate>> findAllUserShippingProfiles([AliasAs("user_id")] string userId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Get("/oauth/request_token")]
        Task<GenericResponse<ShippingTemplate>> requestTemporaryCredentials([AliasAs("scope")] string scopes);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Get("/oauth/scopes")]
        Task<GenericResponse<string>> getScopes();

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //[Post("/listings/createListing")]
        //Task createListing([AliasAs("quantity")] int quantity,
        //    [AliasAs("title")] string title,
        //    [AliasAs("description")] string description,
        //    [AliasAs("price")] float price,
        //    [AliasAs("materials")] string[] materials,
        //    [AliasAs("shipping_template_id")] long shipping_template_id,
        //    [AliasAs("taxonomy_id")] int taxonomy_id,
        //    [AliasAs("shop_section_id")] int shop_section_id,
        //    //[AliasAs("image_ids")] int[] image_ids,
        //    [AliasAs("is_customizable")] bool is_customizable,
        //    [AliasAs("non_taxable")] bool non_taxable,
        //    //[AliasAs("image")] Image image, //TODO: image file upload not supported - see https://www.etsy.com/developers/documentation/getting_started/images#section_uploading_images
        //    [AliasAs("state")] CreateListingState state,
        //    [AliasAs("processing_min")] int processing_min,
        //    [AliasAs("processing_max")] int processing_max,
        //    [AliasAs("tags")] string[] tags,
        //    [AliasAs("who_made")] ListingWhoMade who_made,
        //    [AliasAs("is_supply")] bool is_supply,
        //    [AliasAs("when_made")] ListingWhenMade when_made,
        //    [AliasAs("recipient")] ListingRecipient? recipient,
        //    [AliasAs("occasion")] ListingOcassion? occasion,
        //    [AliasAs("style")] string[] style);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Post("/listings")]
        Task createListing([Body(BodySerializationMethod.UrlEncoded)][Property("EtsyBody")] CreateListing createListing);

    }
}