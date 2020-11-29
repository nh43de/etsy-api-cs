using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EtsyApi.Tests
{
    // set credentials in Conventions.cs
    public class EtsyShould
    {
        [Theory, Conventions]
        public void Login(EtsyService.EtsyService etsyService)
        {
            var r = etsyService.Login();

            
        }


        [Theory, Conventions]
        public void GetOauthTokens(EtsyService.EtsyService etsyService)
        {
            var r = etsyService.GetOauthTokens();


        }

        [Theory, Conventions]
        public async Task Search(EtsyService.EtsyService etsyService)
        {
            var r = await etsyService.Search("monstera deliciosa", 100, 0, 1);


        }


        //[Theory, Conventions]
        //public async Task Test123(EtsyService.EtsyService etsyService)
        //{
        //    var r = await etsyService.Get123(CancellationToken.None);

        //    r.IsSuccess.Should().BeTrue();
        //    r.Data.Should().NotBeEmpty();
        //}
    }
}