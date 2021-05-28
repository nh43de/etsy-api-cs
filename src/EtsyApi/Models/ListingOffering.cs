namespace EtsyApi.Models
{
    public class ListingOffering
    {
        /// <summary>
        /// The numeric ID of this offering.
        /// </summary>
        public int offering_id { get; set; }

        /// <summary>
        /// The price of the product
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// How many of this product are available?
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Is the offering shown to buyers?
        /// </summary>
        public bool is_enabled { get; set; }

        /// <summary>
        /// Has the offering been deleted?
        /// </summary>
        public bool is_deleted { get; set; }
    }
}