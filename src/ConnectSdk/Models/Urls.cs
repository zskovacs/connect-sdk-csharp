namespace ConnectSdk.Models;

public class Urls
{
    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("primary")]
    public bool Primary { get; set; }

    [JsonProperty("href")]
    public string Href { get; set; }
}