namespace EtsyApi.Models
{
    public class ListingVariations
    {
        public int property_id { get; set; }
        public string formatted_name { get; set; }
        public ListingVariationOption[] options { get; set; }
    }
}