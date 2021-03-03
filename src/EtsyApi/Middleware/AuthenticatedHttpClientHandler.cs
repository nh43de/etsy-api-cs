using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using OAuth;

namespace EtsyApi.Middleware
{
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


                //    var r = QueryHelpers.AddQueryString(request.RequestUri.AbsoluteUri, q.ToDictionary(p => p.Key, p => p.Value.ToString()));

                //    request.RequestUri = new Uri(r);
                //}

                //unauthenticated, user specific oauth tokens for modifying data
                {
                    var authHeaders = client.GetAuthorizationHeader();

                    request.Headers.Add("Authorization", authHeaders);

                    //request.Headers.Add("api_key", _auth.ConsumerKey);
                }
            }

            if (_auth.ConsumerKey != null && request.RequestUri.AbsolutePath.Contains("/oauth/scopes") == false && request.RequestUri.AbsolutePath.Contains("shipping/templates") == false)
            {
                //unauthenticated, non user specific
                {
                    var r = QueryHelpers.AddQueryString(request.RequestUri.AbsoluteUri, "api_key", _auth.ConsumerKey);

                    request.RequestUri = new Uri(r);
                }
            }

            var rr = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            var debugStr = await rr.Content.ReadAsStringAsync();

            return rr;
        }
    }
}
