# 1Password Connect Key Vault Secrets configuration provider for Microsoft.Extensions.Configuration

The `1PasswordConnect.Extensions.AspNetCore.Configuration.Secrets` package allows storing configuration values using 1Password Key Vault Secrets.

## Getting started

### Install the package

Install the package with [NuGet][nuget]:

```dotnetcli
dotnet add package 1PasswordConnect.Extensions.AspNetCore.Configuration.Secrets
```

### Prerequisites

You can only access one vault at a time, and within that, the **TITLE** of the elements will be the Key in the Configuration, and the first **PASSWORD** field will be its associated secret. If you want to filter what gets loaded, you can provide **TAGs**, and by specifying these during configuration, only those elements will be loaded.

## Examples

To load initialize configuration from 1Password Key Vault secrets call the `AddOnePasswordKeyVault` on `ConfigurationBuilder`:

```csharp
ConfigurationBuilder builder = new ConfigurationBuilder();

OnePasswordKeyVaultConfigurationOptions options = new()
builder.Configuration.GetSection("OnePasswordKeyVault").Bind(options);
builder.AddOnePasswordKeyVault(options);

IConfiguration configuration = builder.Build();
Console.WriteLine(configuration["MySecret"]);
```

If you want to use this, you need to specify the options in the appsettings.json


```json
{
  "ApiKey": "your_api_key",
  "BaseUrl": "http://your.connect.server",
  "VaultId": "your_vault_id",
  "TagFilter": "only_these_will_load_to_the_config"
}
```

Another way you can initialize the vault:
```csharp
ConfigurationBuilder builder = new ConfigurationBuilder();

builder.AddOnePasswordKeyVault(opt =>
{
  opt.ApiKey = "your_api_key",
  opt.BaseUrl = "http://your.connect.server",
  opt.VaultId = "your_vault_id",
  opt.TagFilter = "only_these_will_load_to_the_config"
});

IConfiguration configuration = builder.Build();
Console.WriteLine(configuration["MySecret"]);
```

<!-- LINKS -->
[source]: https://github.com/zskovacs/connect-sdk-csharp/tree/main/src/ConnectSdk.Extensions.AspNetCore.Configuration.Secrets
[package]: https://www.nuget.org/packages/1PasswordConnect.Extensions.AspNetCore.Configuration.Secrets/
[nuget]: https://www.nuget.org/packages/1PasswordConnect.Extensions.AspNetCore.Configuration.Secrets