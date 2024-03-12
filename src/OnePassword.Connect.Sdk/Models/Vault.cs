namespace OnePassword.Connect.Sdk.Models;

public class Vault
    {
        [JsonProperty("id")]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^[\da-z]{26}$")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The vault version
        /// </summary>
        [JsonProperty("attributeVersion")]
        public int AttributeVersion { get; set; }

        /// <summary>
        /// The version of the vault contents
        /// </summary>
        [JsonProperty("contentVersion")]
        public int ContentVersion { get; set; }

        /// <summary>
        /// Number of active items in the vault
        /// </summary>
        [JsonProperty("items")]
        public int Items { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public VaultType Type { get; set; }

        [JsonProperty("createdAt")]
        public System.DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public System.DateTimeOffset UpdatedAt { get; set; }
    }