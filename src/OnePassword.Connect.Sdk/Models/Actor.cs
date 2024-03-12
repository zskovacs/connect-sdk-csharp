namespace OnePassword.Connect.Sdk.Models;

public class Actor
{
    [JsonProperty("id")]
    public System.Guid Id { get; set; }

    [JsonProperty("account")]
    public string Account { get; set; }

    [JsonProperty("jti")]
    public string Jti { get; set; }

    [JsonProperty("userAgent")]
    public string UserAgent { get; set; }

    [JsonProperty("requestIp")]
    public string RequestIp { get; set; }
    
}