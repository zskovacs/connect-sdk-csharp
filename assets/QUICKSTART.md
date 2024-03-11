# Quickstart

## Creating a Connect API Client

```csharp
var options = new OnePasswordConnectOptions
{
    BaseUrl = "http://192.168.0.2:8080", 
    ApiKey = "my-token"
};

var client = new OnePasswordConnectClient(options);
```

## Creating a Connect API Client with Dependency Injection

```csharp
services.AddOnePasswordConnect(options => {
    options.BaseUrl = "http://192.168.0.2:8080";
    options.ApiKey = "my-token";    
});
```

```csharp
public class MyClass()
{
    private readonly IOnePasswordConnectClient _client;
    
    public MyClass(IOnePasswordConnectClient client)
    {
        _client = client;
    }
}
```

## Working with Vaults

```csharp
// Get a list of all vaults
var vaults = await client.GetVaultsAsync("name eq MY_VAULT_NAME");

// Get a specific vault
var vaultDetails = await client.GetVaultByIdAsync("vault_id");
```

## Working with Items

```csharp
var vaultId = "vault_id";

// Create an Item
var item = new FullItem
{
    Vault = new Vault()
    {
        Id = vaultId
    },
    Category = ItemCategory.LOGIN, 
    Fields = new List<Field>()
    {
        new Field()
        {
            Id = "password",
            Purpose = FieldPurpose.PASSWORD,
            Label = "password", 
            Value = "MySecret"
        }
    }
};

var item = await client.CreateVaultItemAsync(vaultId, item);

// Get an item
var result = await client.GetVaultItemByIdAsync(vaultId, item.Id);

// Update an item
item.Title = "New Item Title";
var updatedItem = await client.UpdateVaultItemAsync(vaultId, item.Id, item)

// Delete an item
await client.DeleteVaultItemAsync(vaultId, item.Id)
```