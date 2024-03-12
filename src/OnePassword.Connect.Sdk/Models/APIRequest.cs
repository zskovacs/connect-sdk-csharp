namespace OnePassword.Connect.Sdk.Models;

public class APIRequest
{
    /// <summary>
    /// The unique id used to identify a single request.
    /// </summary>
    [JsonProperty("requestId")]
    public Guid RequestId { get; set; }

    /// <summary>
    /// The time at which the request was processed by the server.
    /// </summary>
    [JsonProperty("timestamp")]
    public DateTimeOffset Timestamp { get; set; }

    [JsonProperty("action")]
    [JsonConverter(typeof(StringEnumConverter))]
    public APIRequestAction Action { get; set; }

    [JsonProperty("result")]
    [JsonConverter(typeof(StringEnumConverter))]
    public APIRequestResult Result { get; set; }

    [JsonProperty("actor")] public Actor Actor { get; set; }

    [JsonProperty("resource")] public Resource Resource { get; set; }
}