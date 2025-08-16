using System.Text.Json.Serialization;

public class ApiResponse
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ApiStatus Status { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }

    public ApiResponse(ApiStatus status, string message, object data = null)
    {
        Status = status;
        Message = message;
        Data = data;
    }
}