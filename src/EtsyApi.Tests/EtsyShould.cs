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
        private readonly int _listingExample = 962382648;
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
        
        //[Theory, Conventions]
        public async Task CreateListing(EtsyService etsyService)
        {
            //var templates = await etsyService.GetUserShippingProfiles(_testUserShippingProfiles);

            //var template = templates.FirstOrDefault();

            var description =
                    @"Test description"
                ;
            
            var title = "Test title";

            var price = 88.99f;

            await etsyService.CreateListing(new CreateListing
            {
                quantity = 1,
                title = title,
                description = description,
                price = price,
                shipping_template_id = 131526383384,
                taxonomy_id = 6561,
                shop_section_id = 32402445,
                non_taxable = false,
                is_supply = false,
                state = CreateListingState.draft,
                tags = new StringCollection(new[] { "testtag", "tag" }),
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
        public async Task GetListingWithVariations(EtsyService etsyService)
        {
            var r = await etsyService.GetListingsById(new[] { _listingExample }, CancellationToken.None, ListingAssociationIncludes.Variations).ToArrayAsync();
        }

        [Theory, Conventions]
        public async Task GetAllTaxonomies(EtsyService etsyService)
        {
            var r = await etsyService.GetBuyerTaxonomiesFlattened();
            
            r.WriteCsv("EtsyTaxonomies.csv");
        }

        [Theory, Conventions]
        public async Task GetUserShippingProfiles(EtsyService etsyService)
        {
            var r = await etsyService.GetUserShippingProfiles(_testUserShippingProfiles);

            r.WriteCsv("shippingTemplates.csv");

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

        [Theory, Conventions]
        public async Task GetTaxonomy(EtsyService etsyService)
        {
            var r = await etsyService.GetBuyerTaxonomies();
            
        }


        [Theory, Conventions]
        public async Task GetShops(EtsyService etsyService)
        {
            var r = await etsyService.GetShops(new [] {"CMoreDesignsYourway"});
        }

        [Theory, Conventions]
        public async Task SearchSinglePage(EtsyService etsyService)
        {
            var r = await etsyService.GetListingsPage(_searchExample, null, new GetListingsPageParameters
            {
                Limit = 100,
                Offset = 0,
                Page = 1
            });
        }

        [Theory, Conventions]
        public async Task SearchAll(EtsyService etsyService)
        {
            var r = await etsyService.GetAllListings(_searchExample, 891).ToArrayAsync();
        }

        [Theory, Conventions]
        public async Task SearchAllWithIncludes(EtsyService etsyService)
        {
            var r = await etsyService.GetAllListings(_searchExample, 891, new GetListingsPageParameters()
            {
                Includes = "Images(url_75x75,url_170x135):1:0,Shop(shop_id)"
            }).ToArrayAsync();
        }

        [Theory, Conventions]
        public async Task GetAllShopListings(EtsyService etsyService)
        {
            var r = await etsyService.GetAllShopListings(_shopExample).ToArrayAsync();
        }

        [Theory, Conventions]
        public async Task GetAllShopListingsWithIncludes(EtsyService etsyService)
        {
            var r = await etsyService.GetAllShopListings(_shopExample, null, null,new GetListingsPageParameters
            {
                Limit = 100,
                Offset = 0,
                Page = 1,
                Includes = "Images(url_75x75,url_170x135):1:0,Shop(shop_id)"
            }).ToArrayAsync();
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