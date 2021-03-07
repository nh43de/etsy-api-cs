using System.Collections;
using EtsyApi.Models;
using Refit;

namespace EtsyApi
{

    public class CreateListing
    {
        /// <summary>
        /// $5
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public float price { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public StringCollection materials { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public long shipping_template_id { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public int taxonomy_id { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public int shop_section_id { get; set; }

        ///// <summary>
        ///// $5
        ///// </summary>
        //public int[] image_ids { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public bool? is_customizable { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public bool non_taxable { get; set; }

        ///// <summary>
        ///// $5
        ///// </summary>
        //public image image { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public CreateListingState state { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public int processing_min { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public int processing_max { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public StringCollection tags { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public ListingWhoMade who_made { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public bool is_supply { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public ListingWhenMade when_made { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public ListingRecipient recipient { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public ListingOcassion occasion { get; set; }

        /// <summary>
        /// $5
        /// </summary>
        public StringCollection style { get; set; }
    }
}
