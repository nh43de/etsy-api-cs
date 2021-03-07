using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.CompilerServices;
using DataPowerTools.Extensions;
using EtsyApi.Models;
using EtsyApi.Models.Associations;
using FluentAssertions;
using Xunit;

namespace EtsyApi.Tests
{
    // set credentials in Conventions.cs
    public class EtsyShould
    {
        private readonly string _searchExample = "aquarium pedestal";
        private readonly string _shopExample = "AuroraGraceDesign";
        private readonly int _listingExample = 184814873;
        private readonly int _taxonomyIdExample = 6871;
        private readonly string _testUserShippingProfiles = "__SELF__";

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
        public async Task CreateListing(EtsyService etsyService)
        {
            //var templates = await etsyService.GetUserShippingProfiles(_testUserShippingProfiles);

            //var template = templates.FirstOrDefault();
            

            await etsyService.CreateListing(new CreateListing()
            {
                quantity = 1,
                title = "Test Item",
                description = "This is a \"test\" item",
                price = 20.99f,
                shipping_template_id = 131535583384,//template.shipping_template_id,
                taxonomy_id = _taxonomyIdExample,
                //shop_section_id = shopSectionId,
                non_taxable = false, 
                is_supply = false,
                state = CreateListingState.draft,
                tags = new StringCollection(new [] { "test item", "test tag2" }),
                who_made = ListingWhoMade.i_did,
                when_made = ListingWhenMade._2020_2021,
                recipient = ListingRecipient.not_specified,
                processing_max = 2,
                processing_min = 1
            });
        }

        [Theory, Conventions]
        public async Task GetListing(EtsyService etsyService)
        {
            var r = await etsyService.GetListingsById(new[] { _listingExample }).ToArrayAsync();
        }
        
        [Theory, Conventions]
        public async Task GetListingWithIncludes(EtsyService etsyService)
        {
            var r = await etsyService.GetListingsById(new[] { _listingExample }, CancellationToken.None, ListingAssociationIncludes.ShippingInfo | ListingAssociationIncludes.Shop | ListingAssociationIncludes.Images).ToArrayAsync();
        }

        [Theory, Conventions]
        public async Task GetListingWithIncludesString(EtsyService etsyService)
        {
            var r = await etsyService.GetListingsByIdStringIncludes(new[] { _listingExample }, CancellationToken.None, "Images(url_75x75,url_170x135):1:0,Shop(shop_id)").ToArrayAsync();
        }

        [Theory, Conventions]
        public async Task GetAllTaxonomies(EtsyService etsyService)
        {
            var r = await etsyService.GetBuyerTaxonomiesFlattened();
        }

        [Theory, Conventions]
        public async Task GetUserShippingProfiles(EtsyService etsyService)
        {
            var r = await etsyService.GetUserShippingProfiles(_testUserShippingProfiles);
        }


        [Theory, Conventions]
        public async Task GetUserAccessScopes(EtsyService etsyService)
        {
            var r = await etsyService.GetUserAccessScopes();
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
        public async Task GetShops(EtsyService etsyService)
        {
            var r = await etsyService.GetShops(new [] {"CMoreDesignsYourway"});
        }

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