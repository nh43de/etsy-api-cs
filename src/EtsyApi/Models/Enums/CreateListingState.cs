using System.Runtime.Serialization;

namespace EtsyApi.Models
{
    public enum CreateListingState
    {
        [EnumMember(Value = "active")]
        active,
        [EnumMember(Value = "draft")]
        draft
	}
}