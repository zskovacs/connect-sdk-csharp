namespace OnePassword.Connect.Sdk.Models;

public class Field
{
    [JsonProperty("id", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    public string Id { get; set; }

    [JsonProperty("section")]
    public Section Section { get; set; }

    [JsonProperty("type", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    [JsonConverter(typeof(StringEnumConverter))]
    public FieldType Type { get; set; } = FieldType.STRING;

    /// <summary>
    /// Some item types, Login and Password, have fields used for autofill. This property indicates that purpose and is required for some item types.
    /// </summary>
    [JsonProperty("purpose")]
    [JsonConverter(typeof(StringEnumConverter))]
    public FieldPurpose Purpose { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }

    /// <summary>
    /// If value is not present then a new value should be generated for this field
    /// </summary>
    [JsonProperty("generate")]
    public bool Generate { get; set; }

    [JsonProperty("recipe")]
    public GeneratorRecipe Recipe { get; set; }

    /// <summary>
    /// For fields with a purpose of `PASSWORD` this is the entropy of the value
    /// </summary>
    [JsonProperty("entropy")]
    public double Entropy { get; set; }
}