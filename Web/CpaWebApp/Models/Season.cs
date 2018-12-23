using System.Runtime.Serialization;

namespace CpaWebApp.Models
{
    public enum Season
    {
        [EnumMember(Value = "Зима")]
        winter,
        [EnumMember(Value = "Весна")]
        spring,
        [EnumMember(Value = "Лето")]
        summer,
        [EnumMember(Value = "Осень")]
        fall
    }


}