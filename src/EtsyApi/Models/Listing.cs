﻿namespace EtsyApi.Models
{
    public class Listing
    {
        public int? category_id { get; set; }
        public float? creation_tsz { get; set; }
        public string currency_code { get; set; }
        public string description { get; set; }
        public float? ending_tsz { get; set; }
        public bool has_variations { get; set; }
        public bool is_private { get; set; }
        public bool is_supply { get; set; }
        public float? last_modified_tsz { get; set; }
        public int? listing_id { get; set; }
        public int? num_favorers { get; set; }
        public float? original_creation_tsz { get; set; }
        public string price { get; set; }
        public int? quantity { get; set; }
        public long? shipping_template_id { get; set; }
        public int? shop_section_id { get; set; }
        public string[] sku { get; set; }
        public ListingState state { get; set; }
        public float? state_tsz { get; set; }
        public int? suggested_taxonomy_id { get; set; }
        public string[] tags { get; set; }
        public int? taxonomy_id { get; set; }
        public string[] taxonomy_path { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public int? user_id { get; set; }
        public int? views { get; set; }
}
}