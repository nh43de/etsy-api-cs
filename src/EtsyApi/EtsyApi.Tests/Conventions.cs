
using System.IO;
using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Configuration;

namespace EtsyApi.Tests
{

    public class Conventions : AutoDataAttribute
    {
        public Conventions() : base(Create)
        {

        }

        private static IFixture Create()
        {
            var fixture = new Fixture();

            //var auth = new EtsyApiAuth
            //{
            //    ConsumerKey = ConsumerKey,
            //    ConsumerSecret = ConsumerSecret,
            //    TokenSecret = TokenSecret,
            //    Verifier = VerifierSecret,
            //    Token = Token,
            //    OAuthTokenSecret = OAuthTokenSecret,
            //    OAuthToken = OAuthToken
            //};
            
            var appSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("etsyapiauth.user.json")
                .Build();

            var auth = appSettings.GetSection("EtsyApiAuth").Get<EtsyApiAuth>();

            fixture.Inject(new EtsyService.EtsyService(auth));

            return fixture;
        }
    }
}