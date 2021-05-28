namespace EtsyApi.Models
{
    public class ListingVariationOption
    {
        public long value_id { get; set; }
        public string value { get; set; }
        public string formatted_value { get; set; }
        public bool is_available { get; set; }
        public float price_diff { get; set; }
        public float price { get; set; }
    }
}