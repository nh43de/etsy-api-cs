using EtsyApi.Models;

namespace EtsyApi.Responses
{
    public class ShopsResponse
    {
        public int count { get; set; }

        public Shop[] results { get; set; }
    }


}
