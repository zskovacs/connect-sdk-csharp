namespace ConnectSdk.Models;

public class Response
{
    [JsonProperty("name", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    public string Name { get; set; }

    /// <summary>
    /// The Connect server's version
    /// </summary>
    [JsonProperty("version", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    public string Version { get; set; }

    [JsonProperty("dependencies")]
    public ICollection<ServiceDependency> Dependencies { get; set; }



}