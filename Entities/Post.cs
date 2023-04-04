using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Entities
{
    public enum Post
    {
        [EnumMember(Value = "Manager")]
        Manager = 1,
        [EnumMember(Value = "Engineer")]
        Engineer = 2,
        [EnumMember(Value = "RectalSuppositoriesTester")]
        RectalSuppositoriesTester = 3,
    }   
}
