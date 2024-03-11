namespace ConnectSdk.Models;

public class FullItem : Item
{
    [JsonProperty("sections")]
    public ICollection<Section> Sections { get; set; }

    [JsonProperty("fields")]
    public ICollection<Field> Fields { get; set; }

    [JsonProperty("files")]
    public ICollection<File> Files { get; set; }

}