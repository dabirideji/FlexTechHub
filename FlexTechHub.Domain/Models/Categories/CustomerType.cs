using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace InventoryManagement.Domain.Models.Categories
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CustomerType
    {
        [EnumMember(Value = "CORE")]
        CORE,
        [EnumMember(Value = "GOOGLE")]
        GOOGLE,
        [EnumMember(Value = "FACEBOOK")]
        FACEBOOK

    }
}