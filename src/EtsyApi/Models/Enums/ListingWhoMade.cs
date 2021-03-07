using System.Runtime.Serialization;

namespace EtsyApi.Models
{
    public enum ListingWhoMade
    {
        [EnumMember(Value = "i_did")]
        i_did,
        [EnumMember(Value = "collective")]
        collective,
        [EnumMember(Value = "someone_else")]
        someone_else
	}
}