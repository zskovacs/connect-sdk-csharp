namespace OnePassword.Connect.Sdk.Models;

public class File
{
    /// <summary>
    /// ID of the file
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// Name of the file
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Size in bytes of the file
    /// </summary>
    [JsonProperty("size")]
    public int Size { get; set; }

    /// <summary>
    /// Path of the Connect API that can be used to download the contents of this file.
    /// </summary>
    [JsonProperty("content_path")]
    public string ContentPath { get; set; }

    /// <summary>
    /// For files that are in a section, this field describes the section.
    /// </summary>
    [JsonProperty("section")]
    public Section Section { get; set; }

    /// <summary>
    /// Base64-encoded contents of the file. Only set if size &lt;= OP_MAX_INLINE_FILE_SIZE_KB kb and `inline_files` is set to `true`.
    /// </summary>
    [JsonProperty("content")]
    public byte[] Content { get; set; }
}