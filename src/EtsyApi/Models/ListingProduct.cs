namespace EtsyApi.Models
{
    public class ListingProduct
    {
        /// <summary>
        /// The numeric ID of this product.
        /// </summary>
        public int product_id { get; set; }

        ///// <summary>
        ///// A list of 0-2 properties associated with this product and their values.
        ///// </summary>
        //public PropertyValue[] property_values { get; set; }

        /// <summary>
        /// The product identifier (if set).
        /// </summary>
        public string sku { get; set; }

        /// <summary>
        /// A JSON list of active offerings for this product.
        /// </summary>
        public ListingOffering[] offerings { get; set; }

        /// <summary>
        /// Has the product been deleted?
        /// </summary>
        public bool is_deleted { get; set; }
    }
}