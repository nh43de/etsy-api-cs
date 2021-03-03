namespace EtsyApi.Models
{
    public class Shop
    {
        /// <summary>
        /// The shop's numeric ID. 
        /// </summary>
        public int shop_id { get; set; }

        /// <summary>
        /// The shop's name. 
        /// </summary>
        public string shop_name { get; set; }

        /// <summary>
        /// The first line of the shops address. Deprecated: use UserAddress.first_line instead. 
        /// </summary>
        public string first_line { get; set; }

        /// <summary>
        /// The second line of the shops address. Deprecated: use UserAddress.second_line instead.
        /// </summary>
        public string second_line { get; set; }

        /// <summary>
        /// The city the shop is in. Deprecated: use UserAddress.city instead.
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// The state the shop is in. Deprecated: use UserAddress.state instead.
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// The zip code the shop is in. Deprecated: use UserAddress.zip instead.
        /// </summary>
        public string zip { get; set; }

        /// <summary>
        /// The numeric ID of the country the shop is in. Deprecated: use UserAddress.country_id instead.
        /// </summary>
        public int? country_id { get; set; }

        /// <summary>
        /// The numeric ID of the user that owns this shop. 
        /// </summary>
        public int? user_id { get; set; }

        /// <summary>
        /// The date and time the shop was created, in epoch seconds. 
        /// </summary>
        public float creation_tsz { get; set; }

        /// <summary>
        /// A brief heading for the shop's main page. 
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// An announcement to buyers that displays on the shop's homepage. 
        /// </summary>
        public string announcement { get; set; }

        /// <summary>
        /// The ISO code (alphabetic) for the seller's native currency. 
        /// </summary>
        public string currency_code { get; set; }

        /// <summary>
        /// If the seller is not currently accepting purchases, 1, blank otherwise. 
        /// </summary>
        public bool is_vacation { get; set; }

        /// <summary>
        /// If is_vacation=1, a message to buyers. 
        /// </summary>
        public string vacation_message { get; set; }

        /// <summary>
        /// A message that is sent to users who buy from this shop. 
        /// </summary>
        public string sale_message { get; set; }

        /// <summary>
        /// A message that is sent to users who buy a digital item from this shop. 
        /// </summary>
        public string digital_sale_message { get; set; }

        /// <summary>
        /// The date and time the shop was last updated, in epoch seconds. 
        /// </summary>
        public float last_updated_tsz { get; set; }

        /// <summary>
        /// The number of active listings in the shop. 
        /// </summary>
        public int listing_active_count { get; set; }

        /// <summary>
        /// Number of digital listings the shop has. 
        /// </summary>
        public int digital_listing_count { get; set; }

        /// <summary>
        /// The user's login name. 
        /// </summary>
        public string login_name { get; set; }

        /// <summary>
        /// The latitude of the shop. 
        /// </summary>
        public float? lat { get; set; }

        /// <summary>
        /// The longitude of the shop. 
        /// </summary>
        public float? lon { get; set; }

        /// <summary>
        /// True if the shop accepts requests for custom items. 
        /// </summary>
        public bool accepts_custom_requests { get; set; }

        /// <summary>
        /// The introductory text from the top of the seller's policies page (may be blank). 
        /// </summary>
        public string policy_welcome { get; set; }

        /// <summary>
        /// The seller's policy on payment (may be blank). 
        /// </summary>
        public string policy_payment { get; set; }

        /// <summary>
        /// The seller's policy on shipping (may be blank). 
        /// </summary>
        public string policy_shipping { get; set; }

        /// <summary>
        /// The seller's policy on refunds (may be blank). 
        /// </summary>
        public string policy_refunds { get; set; }

        /// <summary>
        /// Any additional policy information the seller provides (may be blank). 
        /// </summary>
        public string policy_additional { get; set; }

        /// <summary>
        /// The shop's seller information (may be blank). 
        /// </summary>
        public string policy_seller_info { get; set; }

        /// <summary>
        /// The date and time the shop was last updated, in epoch seconds. 
        /// </summary>
        public float? policy_updated_tsz { get; set; }

        /// <summary>
        /// True if seller has private info to show in EU receipts 
        /// </summary>
        public bool policy_has_private_receipt_info { get; set; }

        /// <summary>
        /// If is_vacation=1, a message to buyers in response to new convos. 
        /// </summary>
        public string vacation_autoreply { get; set; }

        /// <summary>
        /// The shop's Google Analytics code. 
        /// </summary>
        public string ga_code { get; set; }

        /// <summary>
        /// The shop owner's real name. Deprecated: use UserAddress.name instead.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The URL of the shop 
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// The URL to the shop's banner image. 
        /// </summary>
        public string image_url_760x100 { get; set; }

        /// <summary>
        /// The number of members who've marked this Shop as a favorite 
        /// </summary>
        public int num_favorers { get; set; }

        /// <summary>
        /// The languages that this Shop is enrolled in. 
        /// </summary>
        public string[] languages { get; set; }

        /// <summary>
        /// the id of the next upcoming event this hops is attending, that is near the user. 
        /// </summary>
        public int? upcoming_local_event_id { get; set; }

        /// <summary>
        /// The url of the shop full-sized shop icon. 
        /// </summary>
        public string icon_url_fullxfull { get; set; }

        /// <summary>
        /// True if shop has accepted using structured policies. 
        /// </summary>
        public bool is_using_structured_policies { get; set; }

        /// <summary>
        /// True if shop has viewed structured policies onboarding and accepted OR declined. 
        /// </summary>
        public bool has_onboarded_structured_policies { get; set; }

        /// <summary>
        /// True if shop has any unstructured policy fields filled out. 
        /// </summary>
        public bool has_unstructured_policies { get; set; }

        /// <summary>
        /// Privacy policy information the seller provides (may be blank). 
        /// </summary>
        public string policy_privacy { get; set; }

        /// <summary>
        /// Should this user's listings be created or edited using the new Inventory endpoints? 
        /// </summary>
        public bool? use_new_inventory_endpoints { get; set; }

        /// <summary>
        /// Is this shop US based?
        /// </summary>
        public bool? is_shop_us_based { get; set; }
        
        /// <summary>
        /// True if shop policies include a link to EU online dispute form 
        /// </summary>
        public bool include_dispute_form_link { get; set; }

        
    }
}
