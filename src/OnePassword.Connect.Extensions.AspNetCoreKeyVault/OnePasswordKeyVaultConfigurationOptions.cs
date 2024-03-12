namespace OnePassword.Connect.Extensions.AspNetCoreKeyVault;

/// <summary>
/// Options class used by the <see cref="OnePasswordKeyVaultConfigurationExtension"/>.
/// </summary>
public class OnePasswordKeyVaultConfigurationOptions : OnePasswordConnectOptions
{
    /// <summary>
    /// Id of the vault
    /// </summary>
    public string VaultId { get; set; }
    
    /// <summary>
    /// Filter secrets by tag
    /// </summary>
    public string TagFilter { get; set; }
    
    /// <summary>
    /// Gets or sets the timespan to wait between attempts at polling the Azure Key Vault for changes. <code>null</code> to disable reloading.
    /// </summary>
    public TimeSpan? ReloadInterval { get; set; }
}