namespace OnePassword.Connect.Sdk.Models;

public class ServiceDependency
{
    [JsonProperty("service")]
    public string Service { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Human-readable message for explaining the current state.
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }
}