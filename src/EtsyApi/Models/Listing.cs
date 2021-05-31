using System;
using System.Text.Json.Serialization;
using EtsyApi.Extensions;
using Newtonsoft.Json.Converters;

namespace EtsyApi.Models
{

    public class Listing
    {
        public int? category_id { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? creation_tsz { get; set; }

        public string currency_code { get; set; }
        public string description { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? ending_tsz { get; set; }

        public bool? has_variations { get; set; }
        public bool? is_private { get; set; }
        public bool? is_supply { get; set; }


        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? last_modified_tsz { get; set; }

        public int? listing_id { get; set; }
        public int? num_favorers { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? original_creation_tsz { get; set; }

        public double? price { get; set; }
        public int? quantity { get; set; }
        public long? shipping_template_id { get; set; }
        public int? shop_section_id { get; set; }
        public string[] sku { get; set; }
        public ListingState? state { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? state_tsz { get; set; }

        public int? suggested_taxonomy_id { get; set; }
        public string[] tags { get; set; }
        public int? taxonomy_id { get; set; }
        public string[] taxonomy_path { get; set; }

        [JsonConverter(typeof(EscapeHtmlEncodedStringConverter))]
        public string title { get; set; }

        public string url { get; set; }
        public int? user_id { get; set; }
        public int? views { get; set; }

        public Shop Shop { get; set; }
        public ShippingInfo[] ShippingInfo { get; set; }
        public ListingImage[] Images { get; set; }

        /// <summary>
        /// Inventory data for this Listing. Note: This can only be used with the getListing, createListing, and updateListing methods.
        /// </summary>
        public ListingInventory Inventory { get; set; }

        public ListingWhoMade? who_made { get; set; }

        public ListingWhenMade? when_made { get; set; }

        public ListingRecipient? recipient { get; set; }
        public ListingOcassion? occasion { get; set; }

        public ListingVariations[] Variations { get; set; }

        public override string ToString()
        {
            return title;
        }
    }
}