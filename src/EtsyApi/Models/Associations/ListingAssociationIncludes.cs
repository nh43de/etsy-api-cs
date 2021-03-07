using System;

namespace EtsyApi.Models.Associations
{
    [Flags]
    public enum ListingAssociationIncludes
    {
        None = 0,
        Shop = 1,
        ShippingInfo = 2,
        Images = 4,
        Variations = 8
    }
}
