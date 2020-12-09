using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using DataPowerTools.Extensions;
using FluentAssertions;
using Xunit;

namespace EtsyApi.Tests
{
    // set credentials in Conventions.cs
    public class EtsyShould
    {
        //add your credentials and uncomment

        //TODO: unit tests



        //[Theory, Conventions]
        //public void Login(EtsyService etsyService)
        //{
        //    var r = etsyService.Login();


        //}


        //[Theory, Conventions]
        //public void GetOauthTokens(EtsyService etsyService)
        //{
        //    var r = etsyService.GetOauthTokens();


        //}

        [Theory, Conventions]
        public async Task FindTaxonomy(EtsyService etsyService)
        {
            var r = await etsyService.FindTaxonomyByPath("test");

            r.WriteCsv(@"c:\code\source\test.csv");

        }

        //[Theory, Conventions]
        //public async Task GetTaxonomy(EtsyService etsyService)
        //{
        //    var r = await etsyService.GetBuyerTaxonomies();


        //}

        [Theory, Conventions]
        public async Task SearchSinglePage(EtsyService etsyService)
        {
            var r = await etsyService.GetListingsPage("knitted scarf", null, 100, 0, 1);


        }


        [Theory, Conventions]
        public async Task SearchSinglePageShop(EtsyService etsyService)
        {
            var r = await etsyService.GetAllShopListings("myshop").ToArrayAsync();




        }

        //[Theory, Conventions]
        //public async Task Search(EtsyService etsyService)
        //{
        //    var progressStr = "";

        //    var progressReporter = new Progress<string>((s) => progressStr = s);

        //    var r = etsyService.GetAllListings("knitted scarf", 1544, CancellationToken.None, progressReporter);

        //    var rr = await r.Take(400).ToArrayAsync();

        //    rr.Length.Should().Be(400);
        //    progressStr.Should().Be("Got page 4");
        //}


    }
}