namespace EtsyApi.Responses
{
    public class ShippingTemplate
    {
        /// <summary>
        /// The numeric ID of this shipping template.
        /// </summary>
        public long shipping_template_id { get; set; }

        /// <summary>
        /// The name of this shipping template.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// The numeric ID of the user who owns this shipping template.
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// The minimum number of days for processing the listing.
        /// </summary>
        public long min_processing_days { get; set; }

        /// <summary>
        /// The maximum number of days for processing the listing.
        /// </summary>
        public long max_processing_days { get; set; }

        /// <summary>
        /// Translated display label for processing days.
        /// </summary>
        public string processing_days_display_label { get; set; }

        /// <summary>
        /// The numeric ID of the country from which the listing ships.
        /// </summary>
        public long origin_country_id { get; set; }
    }
}
