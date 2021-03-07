using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using OAuth;
using Refit;

namespace EtsyApi.Middleware
{
    //class RequestPropertyHandler : DelegatingHandler
    //{
    //    public RequestPropertyHandler(HttpMessageHandler innerHandler = null) : base(innerHandler ?? new HttpClientHandler()) { }

    //    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        // See if the request has a the property
    //        if (request.Properties.ContainsKey("SomeKey"))
    //        {
    //            var someProperty = request.Properties["SomeKey"];
    //            //do stuff
    //        }

    //        request.Properties

    //        if (request.Properties.ContainsKey("someOtherKey"))
    //        {
    //            var someOtherProperty = request.Properties["someOtherKey"];
    //            //do stuff
    //        }

    //        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    //    }
    //}

    public class DefaultFormUrlEncodedParameterFormatter : IFormUrlEncodedParameterFormatter
    {
        static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, EnumMemberAttribute?>> EnumMemberCache
            = new();

        public virtual string? Format(object? parameterValue, string? formatString)
        {
            if (parameterValue == null)
                return null;

            var parameterType = parameterValue.GetType();

            EnumMemberAttribute? enummember = null;
            if (parameterType.GetTypeInfo().IsEnum)
            {
                var cached = EnumMemberCache.GetOrAdd(parameterType, t => new ConcurrentDictionary<string, EnumMemberAttribute?>());
                enummember = cached.GetOrAdd(parameterValue.ToString()!, val => parameterType.GetMember(val).First().GetCustomAttribute<EnumMemberAttribute>());
            }

            return string.Format(CultureInfo.InvariantCulture,
                string.IsNullOrWhiteSpace(formatString)
                    ? "{0}"
                    : $"{{0:{formatString}}}",
                enummember?.Value ?? parameterValue);
        }
    }



    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly EtsyApiAuth _auth;

        private readonly DefaultFormUrlEncodedParameterFormatter _defaultFormatter = new();

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
                    Method = request.Method.ToString(),
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




                var etsyFormContent= request.Properties.Keys.Contains("EtsyBody") ? request.Properties["EtsyBody"] : null;

                if (etsyFormContent != null)
                {
                    var formEncodedValues = etsyFormContent.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .Select(prop => new
                            {
                                prop.Name,
                                Value = prop.GetValue(etsyFormContent, null)
                            })
                            .Where(p => p.Value != null)
                            .ToDictionary(prop => prop.Name, prop => _defaultFormatter.Format(prop.Value, null))
                        ;

                    var encodedContent = new FormUrlEncodedContent(formEncodedValues);

                    request.Content = encodedContent;
                    
                    var authHeaders = client.GetAuthorizationHeader(formEncodedValues);

                    request.Headers.Add("Authorization", authHeaders);
                }
                else
                //unauthenticated, user specific oauth tokens for modifying data
                {
                    var authHeaders = client.GetAuthorizationHeader();
                    
                    request.Headers.Add("Authorization", authHeaders);

                    //request.Headers.Add("api_key", _auth.ConsumerKey);
                }
            }

            if (_auth.ConsumerKey != null 
                && request.RequestUri.AbsolutePath.Contains("/oauth/scopes") == false 
                && request.RequestUri.AbsolutePath.Contains("shipping/templates") == false
                && (request.Method == HttpMethod.Post && request.RequestUri.AbsolutePath.Contains("/v2/listings")) == false
                )
            {
                //unauthenticated, non user specific
                {
                    var r = QueryHelpers.AddQueryString(request.RequestUri.AbsoluteUri, "api_key", _auth.ConsumerKey);

                    request.RequestUri = new Uri(r);
                }
            }

            var rr = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            try
            {
                var debugResponseStr = await rr?.Content?.ReadAsStringAsync();

                var task = request?.Content?.ReadAsStringAsync();

                if (task != null)
                {
                    var requestDebugStr = await task;
                }
            }
            catch (Exception e)
            {

            }
            
            return rr;
        }
    }
}
