using System.Text.Json.Serialization;

namespace LoggingBestPract.Application.CQRS;

public abstract class BaseResponse
{
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; }

    [JsonPropertyName("message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Message { get; set; }
    
}