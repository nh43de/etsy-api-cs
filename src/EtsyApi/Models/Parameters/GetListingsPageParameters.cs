namespace EtsyApi
{
    public class GetListingsPageParameters : GetListingsParameters
    {
        public int? Page { get; set; } = 1;
        public int? Limit { get; set; } = 25;
        public int? Offset { get; set; } = 0;
    }
}
