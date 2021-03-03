//using System;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;

//namespace EtsyApi.Models
//{

//    public class Transaction
//    {
//        /// <summary>
//        /// The numeric ID for this transaction.
//        /// </summary>
//        public int transaction_id { get; set; }

//        /// <summary>
//        /// The title of the listing for this transaction.
//        /// </summary>
//        public string title { get; set; }

//        /// <summary>
//        /// The description of the listing for this transaction.
//        /// </summary>
//        public string description { get; set; }

//        /// <summary>
//        /// The numeric ID for the seller of this transaction.
//        /// </summary>
//        public int seller_user_id { get; set; }

//        /// <summary>
//        /// The numeric ID for the buyer of this transaction.
//        /// </summary>
//        public int buyer_user_id { get; set; }

//        /// <summary>
//        /// The date and time the transaction was created, in epoch seconds.
//        /// </summary>
//        public float creation_tsz { get; set; }

//        /// <summary>
//        /// The date and time the transaction was paid, in epoch seconds.
//        /// </summary>
//        public float paid_tsz { get; set; }

//        /// <summary>
//        /// The date and time the transaction was shipped, in epoch seconds.
//        /// </summary>
//        public float shipped_tsz { get; set; }

//        /// <summary>
//        /// The price of the transaction.
//        /// </summary>
//        public float price { get; set; }

//        /// <summary>
//        /// The ISO code (alphabetic) for the seller's native currency.
//        /// </summary>
//        public string currency_code { get; set; }

//        /// <summary>
//        /// The quantity of items in this transaction.
//        /// </summary>
//        public int quantity { get; set; }

//        /// <summary>
//        /// The tags in the listing for this transaction.
//        /// </summary>
//        public string[] tags { get; set; }

//        /// <summary>
//        /// The materials in the listing for this transaction.
//        /// </summary>
//        public string[] materials { get; set; }

//        /// <summary>
//        /// The numeric ID of the primary listing image for this transaction.
//        /// </summary>
//        public int image_listing_id { get; set; }

//        /// <summary>
//        /// The numeric ID for the receipt associated to this transaction.
//        /// </summary>
//        public int receipt_id { get; set; }

//        /// <summary>
//        /// The shipping cost for this transaction.
//        /// </summary>
//        public float shipping_cost { get; set; }

//        /// <summary>
//        /// True if this listing is for a digital download.
//        /// </summary>
//        public bool is_digital { get; set; }

//        /// <summary>
//        /// Written description of the files attached to this digital listing.
//        /// </summary>
//        public string file_data { get; set; }

//        /// <summary>
//        /// The numeric ID for this listing associated to this transaction.
//        /// </summary>
//        public int listing_id { get; set; }

//        /// <summary>
//        /// True if this transaction was created for an in-person quick sale.
//        /// </summary>
//        public bool is_quick_sale { get; set; }

//        /// <summary>
//        /// The numeric ID of seller's feedback.
//        /// </summary>
//        public int seller_feedback_id { get; set; }

//        /// <summary>
//        /// The numeric ID for the buyer's feedback.
//        /// </summary>
//        public int buyer_feedback_id { get; set; }

//        /// <summary>
//        /// The type of transaction, usually "listing".
//        /// </summary>
//        public string transaction_type { get; set; }

//        /// <summary>
//        /// URL of this transaction
//        /// </summary>
//        public string url { get; set; }

//        /// <summary>
//        /// Purchased variations for this transaction.
//        /// </summary>
//        public ListingInventory[] variations { get; set; }

//        /// <summary>
//        /// The product data with the purchased item, if any.
//        /// </summary>
//        public ListingProduct product_data { get; set; }
//    }
//}