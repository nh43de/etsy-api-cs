﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Web;
using EtsyApi.Extensions;
using EtsyApi.Middleware;
using EtsyApi.Models;
using EtsyApi.Models.Associations;
using EtsyApi.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OAuth;
using Refit;

namespace EtsyApi
{
    public class EtsyService
    {
        private readonly EtsyApiAuth _auth;

        private readonly Uri _baseUri = new Uri("https://openapi.etsy.com/v2");

        private readonly IEtsyApi _etsyApi;

        public EtsyService(EtsyApiAuth auth)
        {

            //var options = new JsonSerializerOptions()
            //{
            //    NumberHandling = JsonNumberHandling.AllowReadingFromString
            //};

            //options.Converters.Add(new JsonStringEnumConverter());
            //options.Converters.Add(new SystemObjectNewtonsoftCompatibleConverter());

            //_auth = auth;
            //var httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new AuthenticatedHttpClientHandler(auth))) { BaseAddress = _baseUri };
            //_etsyApi = RestService.For<IEtsyApi>(httpClient, new RefitSettings()
            //{
            //    ContentSerializer = new SystemTextJsonContentSerializer(options)
            //});
            //

            var options = new JsonSerializerSettings()
            {
            };
            options.Converters.Add(new UnixDateTimeConverter());
            options.Converters.Add(new EscapeHtmlEncodedStringConverter());

            _auth = auth;
            var httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new AuthenticatedHttpClientHandler(auth))) { BaseAddress = _baseUri };
            _etsyApi = RestService.For<IEtsyApi>(httpClient, new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(options)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="taxonomyId"></param>
        /// <returns></returns>
        public async Task<ListingSearchResult> GetListingsPage(string keyword, int? taxonomyId = null, GetListingsPageParameters pageParams = null)
        {
            return await _etsyApi
                .findAllListingActive(keyword, taxonomyId,
                    pageParams?.Limit ?? 100,
                    pageParams?.Offset ?? 0,
                    pageParams?.Page ?? 1,
                    pageParams?.Includes ?? null)
                .ConfigureAwait(false);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="keyword"></param>
        /// <param name="taxonomyId"></param>
        /// <param name="pageParams"></param>
        /// <returns></returns>
        public async Task<ListingSearchResult> GetShopListingsPage(
            string shopId,
            string keyword = null,
            int? taxonomyId = null,
            GetListingsPageParameters pageParams = null)
        {
            return await _etsyApi
                .findAllShopListingsActive(shopId, keyword, taxonomyId,
                    pageParams?.Limit ?? 100,
                    pageParams?.Offset ?? 0,
                    pageParams?.Page ?? 1,
                    pageParams?.Includes ?? null)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets listings by ID for the page.
        /// </summary>
        /// <param name="listingId"></param>
        /// <param name="includes">Listing associations to include.</param>
        /// <returns></returns>
        public Task<ListingSearchResult> GetListingsByIdPage(int[] listingId, ListingAssociationIncludes includes = ListingAssociationIncludes.None)
        {
            var includesStr = includes.ToIncludeString();

            return GetListingsByIdPage(listingId, includesStr);
        }

        /// <summary>
        /// Gets listings by ID for the page.
        /// </summary>
        /// <param name="listingId"></param>
        /// <param name="includesStr">Listing associations string to include.</param>
        /// <returns></returns>
        public async Task<ListingSearchResult> GetListingsByIdPage(int[] listingId, string includesStr)
        {
            var listingIdStr = string.Join(",", listingId);
            
            return await _etsyApi
                .getListing(listingIdStr, includesStr)
                .ConfigureAwait(false);
        }


        public async Task<Shop[]> GetShops(string[] shopIds)
        {
            var listingIdStr = string.Join(",", shopIds);

            var r = await _etsyApi
                .getShops(listingIdStr)
                .ConfigureAwait(false);

            return r.results;
        }

        /// <summary>
        /// E.g. "outdoor-and-garden"
        /// </summary>
        /// <param name="userId">User Id (found at the end of your profile URL)</param>
        /// <returns></returns>
        public async Task<ShippingTemplate[]> GetUserShippingProfiles(string userId)
        {
            var d = await _etsyApi.findAllUserShippingProfiles(userId);

            return d.results;
        }

        /// <summary>
        /// Async enumerable for enumerating through paginated response.
        /// </summary>
        /// <param name="searchFunc"></param>
        /// <param name="token"></param>
        /// <param name="progressReporter"></param>
        /// <returns></returns>
        private async IAsyncEnumerable<Listing> GetAllInternal(
            Func<int, Task<ListingSearchResult>> searchFunc, 
            [EnumeratorCancellation] CancellationToken token = default(CancellationToken),
            IProgress<string> progressReporter = null)
        {
            var page = 1;
            var hasMorePages = true;

            // Stop with 10 pages, because these are large repos:
            while (hasMorePages)
            {
                if (token.IsCancellationRequested)
                    break;

                progressReporter?.Report("Getting page " + page);

                var r = await searchFunc(page);

                progressReporter?.Report("Got page " + page);

                hasMorePages = r.pagination.next_page.HasValue;

                if (token.IsCancellationRequested)
                    break;

                foreach (var rResult in r.results)
                {
                    yield return rResult;
                }

                page++;
            }
        }

        /// <summary>
        /// Gets all listings and automatically paginates through them.
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="keyword"></param>
        /// <param name="taxonomyId"></param>
        /// <param name="getListingsParameters"></param>
        /// <param name="token"></param>
        /// <param name="progressReporter"></param>
        /// <returns></returns>
        public IAsyncEnumerable<Listing> GetAllShopListings(
            string shopId,
            string keyword = null,
            int? taxonomyId = null,
            GetListingsParameters getListingsParameters = null,
            CancellationToken token = default(CancellationToken),
            IProgress<string> progressReporter = null)
        {
            var rr = GetAllInternal(async (page) =>
            {
                var r = await GetShopListingsPage(shopId, keyword, taxonomyId, new GetListingsPageParameters
                {
                    Limit = 100,
                    Offset = 0,
                    Includes = getListingsParameters?.Includes,
                    Page = page
                });

                return r;
            }, token, progressReporter);

            return rr;
        }

        /// <summary>
        /// Gets all listings and automatically paginates through them.
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="taxonomyId"></param>
        /// <param name="getListingsParameters"></param>
        /// <param name="token"></param>
        /// <param name="progressReporter"></param>
        /// <returns></returns>
        public IAsyncEnumerable<Listing> GetAllListings(
            string keyword,
            int? taxonomyId = null,
            GetListingsParameters getListingsParameters = null,
            CancellationToken token = default(CancellationToken),
            IProgress<string> progressReporter = null)
        {
            var rr = GetAllInternal(async (page) =>
            {
                var r = await GetListingsPage(keyword, taxonomyId, new GetListingsPageParameters
                {
                    Limit = 100,
                    Offset = 0,
                    Includes = getListingsParameters?.Includes,
                    Page = page
                }); //100, 0, page, includes);

                return r;
            }, token, progressReporter);

            return rr;
        }

        /// <summary>
        /// Gets all listings by Id
        /// </summary>
        /// <param name="listingId"></param>
        /// <param name="token"></param>
        /// <param name="includes">Associations to include.</param>
        /// <param name="progressReporter"></param>
        /// <returns></returns>
        public IAsyncEnumerable<Listing> GetListingsById(
            int listingId,
            CancellationToken token = default(CancellationToken),
            ListingAssociationIncludes includes = ListingAssociationIncludes.None,
            IProgress<string> progressReporter = null) => GetListingsById(new[] { listingId }, token, includes, progressReporter);

        /// <summary>
        /// Gets all listings by Id
        /// </summary>
        /// <param name="listingId"></param>
        /// <param name="token"></param>
        /// <param name="includes">Associations to include.</param>
        /// <param name="progressReporter"></param>
        /// <returns></returns>
        public IAsyncEnumerable<Listing> GetListingsById(
            int[] listingId,
            CancellationToken token = default(CancellationToken),
            ListingAssociationIncludes includes = ListingAssociationIncludes.None, 
            IProgress<string> progressReporter = null) => GetListingsByIdStringIncludes(listingId, token, includes.ToIncludeString(), progressReporter);
        
        /// <summary>
        /// Gets all listings by Id
        /// </summary>
        /// <param name="listingId"></param>
        /// <param name="token"></param>
        /// <param name="includes">Listing associations to include.</param>
        /// <param name="progressReporter"></param>
        /// <returns></returns>
        public IAsyncEnumerable<Listing> GetListingsByIdStringIncludes(
            int[] listingId, 
            CancellationToken token = default(CancellationToken),
            string includes = null,
            IProgress<string> progressReporter = null)
        {
            var rr = GetAllInternal(async (page) =>
            {
                var r = await GetListingsByIdPage(listingId, includes);

                return r;
            }, token, progressReporter);

            return rr;
        }
        
        public async Task CreateListing(CreateListing createListing)
        {
            await _etsyApi.createListing(createListing);
        }

        //public async Task CreateListing(int quantity,
        //    string title,
        //    string description,
        //    float price,
        //    string[] materials,
        //    long shipping_template_id,
        //    int taxonomy_id,
        //    int shop_section_id,
        //    bool is_customizable,
        //    bool non_taxable,
        //    CreateListingState state,
        //    int processing_min,
        //    int processing_max,
        //    string[] tags,
        //    ListingWhoMade who_made,
        //    bool is_supply,
        //    ListingWhenMade when_made,
        //    ListingRecipient? recipient,
        //    ListingOcassion? occasion,
        //    string[] style)
        //{
        //    await _etsyApi.createListing(quantity,
        //        title,
        //        description,
        //        price,
        //        materials,
        //        shipping_template_id,
        //        taxonomy_id,
        //        shop_section_id,
        //        is_customizable,
        //        non_taxable,
        //        state,
        //        processing_min,
        //        processing_max,
        //        tags,
        //        who_made,
        //        is_supply,
        //        when_made,
        //        recipient,
        //        occasion,
        //        style);
        //}

        //public async Task OpenOrders()
        //{
        //    const string requestUrl = "https://openapi.etsy.com/v2/shops/YOURSHOPNAME/receipts/open?";

        //    var client = new OAuthRequest
        //    {
        //        Method = "GET",
        //        Type = OAuthRequestType.ProtectedResource,
        //        SignatureMethod = OAuthSignatureMethod.HmacSha1,
        //        ConsumerKey = _consumerKey,
        //        ConsumerSecret = _consumerSecret,
        //        Token = OAuthToken,
        //        TokenSecret = OAuthTokenSecret,
        //        RequestUrl = requestUrl,
        //    };

        //    var url = requestUrl + client.GetAuthorizationQuery();

        //}

        private const string TokenRequestUrl = "https://openapi.etsy.com/v2/oauth/request_token";

        /// <summary>
        /// Take this login URL and accept using logged in Etsy account to register this service. Also note your token secrets
        /// and add to your configuration.
        /// </summary>
        /// <returns></returns>
        public (string token, string tokenSecret, string loginUrl) Login()
        {
            var scopeEncoded = HttpUtility.UrlEncode("email_r listings_r listings_w transactions_r profile_r");

            // Configure our OAuth client
            var client = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = _auth.ConsumerKey,
                ConsumerSecret = _auth.ConsumerSecret,
                RequestUrl = TokenRequestUrl + $"?scope={scopeEncoded}"
                //CallbackUrl = "https://localhost:5001/api/etsy/callback"
            };
            
            //var scopeEncoded = HttpUtility.UrlEncode("email_r,listings_r,listings_w,transactions_r,billing_r,profile_r");


            // Build request url and send the request
            //var url = client.RequestUrl + client.GetAuthorizationQuery();
            var url = client.RequestUrl + "&" + client.GetAuthorizationQuery();
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            


            using var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();

            // Parse login_url and oauth_token_secret from response
            var loginUrl = HttpUtility.ParseQueryString(responseFromServer).Get("login_url");
            var tokenSecret = HttpUtility.ParseQueryString(responseFromServer).Get("oauth_token_secret");
            var token = HttpUtility.ParseQueryString(responseFromServer).Get("oauth_token");

            return (token, tokenSecret, loginUrl);
        }

        public async Task<string[]> GetUserAccessScopes()
        {
            var r = await _etsyApi.getScopes();
            return r.results;
        }

        private const string RequestAccessTokenUrl = "https://openapi.etsy.com/v2/oauth/access_token";

        /// <summary>
        /// Gets tokens for login.
        /// </summary>
        /// <returns></returns>
        public (string OAuthToken, string OAuthTokenSecret) GetOauthTokens()
        {
            if (string.IsNullOrEmpty(_auth.TokenSecret) || string.IsNullOrEmpty(_auth.VerifierSecret))
                throw new Exception("Need to be logged in first.");

            //Create access token request
            var client = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = _auth.ConsumerKey,
                ConsumerSecret = _auth.ConsumerSecret,
                Token = _auth.Token,
                TokenSecret = _auth.TokenSecret,
                RequestUrl = RequestAccessTokenUrl,
                Verifier = _auth.VerifierSecret
            };

            //Build request url and send the request
            var url = client.RequestUrl + "?" + client.GetAuthorizationQuery();
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();

            //Parse and save access token and secret
            var OAuthToken = HttpUtility.ParseQueryString(responseFromServer).Get("oauth_token");
            var OAuthTokenSecret = HttpUtility.ParseQueryString(responseFromServer).Get("oauth_token_secret");

            return (OAuthToken, OAuthTokenSecret);
        }

        public async Task<TaxonomyResponse> GetBuyerTaxonomies()
        {
            return await _etsyApi.getBuyerTaxonomy();
        }

        /// <summary>
        /// E.g. "Outdoor & Gardening"
        /// </summary>
        /// <param name="descriptionContains"></param>
        /// <returns></returns>
        public async Task<Taxonomy[]> FindTaxonomyByName(string descriptionContains)
        {
            var d = await GetBuyerTaxonomies();

            return d.results.Where(p => p.name.Contains(descriptionContains)).ToArray();
        }

        /// <summary>
        /// E.g. "outdoor-and-garden"
        /// </summary>
        /// <param name="pathContains"></param>
        /// <returns></returns>
        public async Task<Taxonomy[]> FindTaxonomyByPath(string pathContains)
        {
            var d = await GetBuyerTaxonomies();

            var all = DescendantsAndSelf(d.results);

            return all.Where(p => p.path.Contains(pathContains)).ToArray();
        }

        /// <summary>
        /// E.g. "outdoor-and-garden"
        /// </summary>
        /// <param name="pathContains"></param>
        /// <returns></returns>
        public async Task<Taxonomy[]> GetBuyerTaxonomiesFlattened()
        {
            var d = await GetBuyerTaxonomies();

            var all = DescendantsAndSelf(d.results);

            return all.ToArray();
        }
        
        /// <summary>
        /// E.g. "outdoor-and-garden"
        /// </summary>
        /// <param name="taxonomyId"></param>
        /// <returns></returns>
        public async Task<Taxonomy[]> FindTaxonomyById(int taxonomyId)
        {
            var d = await GetBuyerTaxonomies();

            var all = DescendantsAndSelf(d.results);

            return all.Where(p => p.id == taxonomyId).ToArray();
        }

        public IEnumerable<Taxonomy> DescendantsAndSelf(IEnumerable<Taxonomy> taxa)
        {
            foreach (var taxonomy in taxa)
            {
                yield return taxonomy;

                foreach (var item in DescendantsAndSelf(taxonomy.children))
                {
                    yield return item;
                }
            }
        }
    }

    

}
