using Newtonsoft.Json;
using Pubg.Net.Infrastructure.Attributes;
using Pubg.Net.Infrastructure.JsonConverters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Pubg.Net
{
    [JsonConverter(typeof(DefaultValueStringEnumConverter))]
    public enum PubgMap
    {
        //In some of the Telemetry they return an empty string
        [DefaultEnumMember] Unspecified,
        [EnumMember(Value = "Baltic_Main")]
        [Description("Erangel (Remastered)")] 
        Erangel_Remastered,
        [EnumMember(Value = "Chimera_Main")]
        [Description("Paramo")]
        Paramo,
        [EnumMember(Value = "Desert_Main")]
        [Description("Miramar")]
        Miramar,
        [EnumMember(Value = "DihorOtok_Main")]
        [Description("Vikendi")]
        Vikendi,
        [EnumMember(Value = "Erangel_Main")]
        [Description("Erangel")]
        Erangel,
        [EnumMember(Value = "Heaven_Main")]
        [Description("Haven")]
        Haven,
        [EnumMember(Value = "Range_Main")]
        [Description("Camp Jackal")]
        Camp_Jackal,
        [EnumMember(Value = "Savage_Main")]
        [Description("Sanhok")]
        Sanhok,
        [EnumMember(Value = "Summerland_Main")]
        [Description("Karakin")]
        Karakin,
        [EnumMember(Value = "Tiger_Main")]
        [Description("Taego")]
        Taego
    }
}
