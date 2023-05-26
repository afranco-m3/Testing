using System.Text.Json.Serialization;

namespace WebAPI.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TypeUser
    {
        Admin = 1,
        Basic = 2,
        ReadOnly = 3
    }
}
