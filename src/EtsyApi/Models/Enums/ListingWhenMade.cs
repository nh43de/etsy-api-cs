using System.Runtime.Serialization;

namespace EtsyApi.Models
{
    public enum ListingWhenMade
    {
        [EnumMember(Value = "made_to_order")]
        _made_to_order,
        [EnumMember(Value = "2020_2021")]
        _2020_2021,
        [EnumMember(Value = "2010_2019")]
        _2010_2019,
        [EnumMember(Value = "2002_2009")]
        _2002_2009,
        [EnumMember(Value = "before_2002")]
        _before_2002,
        [EnumMember(Value = "2000_2001")]
        _2000_2001,
        [EnumMember(Value = "1990s")]
        _1990s,
        [EnumMember(Value = "1980s")]
        _1980s,
        [EnumMember(Value = "1970s")]
        _1970s,
        [EnumMember(Value = "1960s")]
        _1960s,
        [EnumMember(Value = "1950s")]
        _1950s,
        [EnumMember(Value = "1940s")]
        _1940s,
        [EnumMember(Value = "1930s")]
        _1930s,
        [EnumMember(Value = "1920s")]
        _1920s,
        [EnumMember(Value = "1910s")]
        _1910s,
        [EnumMember(Value = "1900s")]
        _1900s,
        [EnumMember(Value = "1800s")]
        _1800s,
        [EnumMember(Value = "1700s")]
        _1700s,
        [EnumMember(Value = "before_1700")]
        _before_1700,
	}
}