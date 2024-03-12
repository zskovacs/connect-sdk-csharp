namespace OnePassword.Connect.Sdk.Models;

public class Anonymous
{
    [JsonProperty("op", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    [JsonConverter(typeof(StringEnumConverter))]
    public Op Op { get; set; }

    /// <summary>
    /// An RFC6901 JSON Pointer pointing to the Item document, an Item Attribute, and Item Field by Field ID, or an Item Field Attribute
    /// </summary>
    [JsonProperty("path", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    public string Path { get; set; }

    [JsonProperty("value")]
    public object Value { get; set; }
    
}