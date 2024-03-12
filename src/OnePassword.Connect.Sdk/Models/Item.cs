namespace OnePassword.Connect.Sdk.Models;

public partial class Item
{
    [JsonProperty("id")]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^[\da-z]{26}$")]
    public string Id { get; set; }

    [JsonProperty("title")] public string Title { get; set; }

    [JsonProperty("vault", Required = Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    public Vault Vault { get; set; } = new Vault();

    [JsonProperty("category", Required = Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public ItemCategory Category { get; set; }

    [JsonProperty("urls")] public System.Collections.Generic.ICollection<Urls> Urls { get; set; }

    [JsonProperty("favorite")] public bool Favorite { get; set; } = false;

    [JsonProperty("tags")] public System.Collections.Generic.ICollection<string> Tags { get; set; }

    [JsonProperty("version")] public int Version { get; set; }

    [JsonProperty("state")]
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public ItemState State { get; set; }

    [JsonProperty("createdAt")] public System.DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("updatedAt")] public System.DateTimeOffset UpdatedAt { get; set; }

    [JsonProperty("lastEditedBy")] public string LastEditedBy { get; set; }
}