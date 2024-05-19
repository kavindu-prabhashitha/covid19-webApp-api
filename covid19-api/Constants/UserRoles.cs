
using System.Text.Json.Serialization;

namespace covid19_api.Constants
{

    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRoles
    {
        ADMINISTRATOR=0,
        USER=1
    }
}