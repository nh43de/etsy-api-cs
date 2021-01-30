using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.CompilerServices;
using DataPowerTools.Extensions;
using FluentAssertions;
using Xunit;

namespace EtsyApi.Tests
{
    // set credentials in Conventions.cs
    public class EtsyShould
    {
        private readonly string _searchExample = "knitted scarf";
        private readonly string _shopExample = "myshop";
        private readonly int _listingExample = 123123123;
        private readonly int _taxonomyIdExample = 123123123;
        
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
        public async Task GetListing(EtsyService etsyService)
        {
            var r = await etsyService.GetListingsById(new[] { _listingExample }).ToArrayAsync();
        }
        
        [Theory, Conventions]
        public async Task GetAllTaxonomies(EtsyService etsyService)
        {
            var r = await etsyService.GetBuyerTaxonomiesFlattened();
        }
        
        [Theory, Conventions]
        public async Task FindTaxonomy(EtsyService etsyService)
        {
            var r = await etsyService.FindTaxonomyByPath("test");
        }

        [Theory, Conventions]
        public async Task FindTaxonomyById(EtsyService etsyService)
        {
            var r = await etsyService.FindTaxonomyById(_taxonomyIdExample);
        }

        //[Theory, Conventions]
        //public async Task GetTaxonomy(EtsyService etsyService)
        //{
        //    var r = await etsyService.GetBuyerTaxonomies();
        //}

        [Theory, Conventions]
        public async Task SearchSinglePage(EtsyService etsyService)
        {
            var r = await etsyService.GetListingsPage(_searchExample, null, 100, 0, 1);
        }

        [Theory, Conventions]
        public async Task SearchAll(EtsyService etsyService)
        {
            var r = await etsyService.GetAllListings(_searchExample, 891).ToArrayAsync();
        }

        [Theory, Conventions]
        public async Task GetAllShopListings(EtsyService etsyService)
        {
            var r = await etsyService.GetAllShopListings(_shopExample).ToArrayAsync();
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