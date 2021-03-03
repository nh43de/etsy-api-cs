namespace EtsyApi.Models
{
    public class ShippingInfo
    {
        /// <summary>
        /// The numeric ID of this shipping info record.
        /// </summary>
        public long shipping_info_id { get; set; }

        /// <summary>
        /// The numeric ID of the country from which the listing ships.
        /// </summary>
        public long? origin_country_id { get; set; }

        /// <summary>
        /// The numeric ID of the country to which the listing ships (optional). If missing, these fees apply to all destinations.
        /// </summary>
        public long? destination_country_id { get; set; }

        /// <summary>
        /// Identifies the currency unit for shipping fees (currently, always 'USD').
        /// </summary>
        public string currency_code { get; set; }

        /// <summary>
        /// The shipping fee for this item, if shipped alone.
        /// </summary>
        public float? primary_cost { get; set; }

        /// <summary>
        /// The shipping fee for this item, if shipped with another item.
        /// </summary>
        public float? secondary_cost { get; set; }

        /// <summary>
        /// The numeric ID of the listing to which this shipping info applies.
        /// </summary>
        public long? listing_id { get; set; }

        /// <summary>
        /// The numeric ID of the region to which this shipping info applies (optional). If missing, no region is selected. Regions are shorthand for lists of individual countries.
        /// </summary>
        public long? region_id { get; set; }

        /// <summary>
        /// The name of the country from which this item ships.
        /// </summary>
        public string origin_country_name { get; set; }

        /// <summary>
        /// The name of the country to which this item ships.
        /// </summary>
        public string destination_country_name { get; set; }
    }
}
