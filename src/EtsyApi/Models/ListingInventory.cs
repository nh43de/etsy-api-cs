using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace EtsyApi.Models
{
    public class ListingInventory
    {
        /// <summary>
        /// The products available for this listing.
        /// </summary>
        public ListingProduct[] products { get; set; }

        /// <summary>
        /// Which properties control price?
        /// </summary>
        public int[] price_on_property { get; set; }

        /// <summary>
        /// Which properties control quantity?
        /// </summary>
        public int[] quantity_on_property { get; set; }

        /// <summary>
        /// Which properties control SKU?
        /// </summary>
        public int[] sku_on_property { get; set; }
        
        public override string ToString()
        {
            return this.ToJson();
        }
    }

}