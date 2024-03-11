namespace ConnectSdk.Models;

public class Resource
{
    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public ResourceType Type { get; set; }

    [JsonProperty("vault")]
    public Vault Vault { get; set; }

    [JsonProperty("item")]
    public Item Item { get; set; }

    [JsonProperty("itemVersion")]
    public int ItemVersion { get; set; }
}