namespace ConnectSdk.Models;

public class ErrorResponse
{
    /// <summary>
    /// HTTP Status Code
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// A message detailing the error
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }
}