using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using EtsyApi.Middleware;
using EtsyApi.Models;
using Microsoft.AspNetCore.WebUtilities;
using OAuth;
using Refit;

namespace EtsyApi.EtsyService
{
    public class EtsyService
    {
        private readonly EtsyApiAuth _auth;

        private readonly Uri _baseUri = new Uri("https://openapi.etsy.com/v2");

        private readonly IEtsyApi _etsyApi;

        public EtsyService(EtsyApiAuth auth)
        {
            _auth = auth;
            var httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new AuthenticatedHttpClientHandler(auth))) { BaseAddress = _baseUri };
            _etsyApi = RestService.For<IEtsyApi>(httpClient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="limit">Max is 100</param>
        /// <param name="offset"></param>
        /// <param name="page">Starts at 1</param>
        /// <returns></returns>
        public async Task<ListingSearchResult> Search(string keywords,int? limit = 25,int? offset = 0,int? page = 1)
        {
            return await _etsyApi
                .findAllListingActive(keywords,limit,offset,page)
                .ConfigureAwait(false);
        }

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
            // Configure our OAuth client
            var client = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = _auth.ConsumerKey,
                ConsumerSecret = _auth.ConsumerSecret,
                RequestUrl = TokenRequestUrl,
                //CallbackUrl = "https://localhost:5001/api/etsy/callback"
            };

            // Build request url and send the request
            var url = client.RequestUrl + "?" + client.GetAuthorizationQuery();
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

        private const string RequestAccessTokenUrl = "https://openapi.etsy.com/v2/oauth/access_token";

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
    }


    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly EtsyApiAuth _auth;
        
        public AuthenticatedHttpClientHandler(EtsyApiAuth auth)//(string consumerKey, string consumerSecret, string tokenSecret, string verifier)
        {
            _auth = auth;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_auth.OAuthToken != null)
            {
                var client = new OAuthRequest
                {
                    Method = "GET",
                    Type = OAuthRequestType.ProtectedResource,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                    ConsumerKey = _auth.ConsumerKey,
                    ConsumerSecret = _auth.ConsumerSecret,
                    Token = _auth.OAuthToken,
                    TokenSecret = _auth.OAuthTokenSecret,
                    RequestUrl = request.RequestUri.GetLeftPart(UriPartial.Path)
                };

                //var httpMethod = request.Method;

                //request.Headers.Add("Authorization", );

                //if (httpMethod != HttpMethod.Get && queryStringParams != null)
                //{
                //    authHeader = client.GetAuthorizationHeader(queryStringParams);
                //}
                //else
                //{
                //    authHeader = client.GetAuthorizationHeader();
                //}

                //{
                //    var authQuery = client.GetAuthorizationQuery();

                //    var q = QueryHelpers.ParseQuery(authQuery);

                //    var r = QueryHelpers.AddQueryString(request.RequestUri.AbsoluteUri, q.ToDictionary(p => p.Key, p => p.Value.ToString()));

                //    request.RequestUri = new Uri(r);
                //}

                //unauthenticated, user specific oauth tokens for modifying data
                {
                    var authHeaders = client.GetAuthorizationHeader();

                    request.Headers.Add("Authorization", authHeaders);

                    request.Headers.Add("api_key", _auth.ConsumerKey);
                }
            }

            if (_auth.ConsumerKey != null)
            {
                //unauthenticated, non user specific
                {
                    var r = QueryHelpers.AddQueryString(request.RequestUri.AbsoluteUri, "api_key", _auth.ConsumerKey);

                    request.RequestUri = new Uri(r);
                }
            }


            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
