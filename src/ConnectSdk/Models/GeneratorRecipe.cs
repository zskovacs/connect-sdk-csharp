namespace ConnectSdk.Models;

public class GeneratorRecipe
{
    /// <summary>
    /// Length of the generated value
    /// </summary>
    [JsonProperty("length")]
    [System.ComponentModel.DataAnnotations.Range(1, 64)]
    public int Length { get; set; } = 32;

    [JsonProperty("characterSets", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore, ItemConverterType = typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public System.Collections.Generic.ICollection<CharacterSets> CharacterSets { get; set; }

    /// <summary>
    /// List of all characters that should be excluded from generated passwords.
    /// </summary>
    [JsonProperty("excludeCharacters")]
    public string ExcludeCharacters { get; set; }
}