using ConnectSdk.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectSdk.Extensions.AspNetCore.Configuration.Secrets;

public class OnePasswordKeyVaultConfigurationProvider : ConfigurationProvider, IDisposable
{
    private readonly TimeSpan? _reloadInterval;
    private readonly CancellationTokenSource _cancellationToken;
    private readonly IOnePasswordConnectClient _client;
    private readonly OnePasswordKeyVaultConfigurationOptions _options;
    private readonly KeyVaultSecretManager _manager = KeyVaultSecretManager.Instance;
    private Dictionary<string, FullItem> _loadedSecrets;
    private Task _pollingTask;
    private bool _disposed;

    public OnePasswordKeyVaultConfigurationProvider(IOnePasswordConnectClient client, OnePasswordKeyVaultConfigurationOptions options = null)
    {
        _options = options ?? throw new ArgumentException(nameof(options));
        _client = client ?? throw new ArgumentException(nameof(client));

        if (options.ReloadInterval != null && options.ReloadInterval.Value <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(options.ReloadInterval), options.ReloadInterval, nameof(options.ReloadInterval) + " must be positive.");
        }

        _pollingTask = null;
        _cancellationToken = new CancellationTokenSource();
        _reloadInterval = options.ReloadInterval;
    }

    public override void Load()
    {
        ICollection<Item> secrets;

        if (string.IsNullOrWhiteSpace(_options.TagFilter))
        {
            secrets = _client.GetVaultItemsAsync(_options.VaultId, "").GetAwaiter().GetResult();
        }
        else
        {
            secrets = _client.GetVaultItemsAsync(_options.VaultId, $"tag eq {_options.TagFilter}").GetAwaiter().GetResult();
        }

        using var secretLoader = new ParallelSecretLoader(_client);
        var newLoadedSecrets = new Dictionary<string, FullItem>();
        var oldLoadedSecrets = Interlocked.Exchange(ref _loadedSecrets, null);

        foreach (var secret in secrets)
        {
            AddSecretToLoader(secret, oldLoadedSecrets, newLoadedSecrets, secretLoader);
        }

        var loadedSecret = secretLoader.WaitForAll();
        UpdateSecrets(loadedSecret, newLoadedSecrets, oldLoadedSecrets);
        
        // schedule a polling task only if none exists and a valid delay is specified
        if (_pollingTask is null && _reloadInterval is not null)
        {
            _pollingTask = PollForSecretChangesAsync();
        }
    }

    private void AddSecretToLoader(Item secret, IDictionary<string, FullItem> oldLoadedSecrets, IDictionary<string, FullItem> newLoadedSecrets, ParallelSecretLoader secretLoader)
    {
        var secretId = secret.Id;
        if (oldLoadedSecrets != null && oldLoadedSecrets.TryGetValue(secretId, out var existingSecret) && IsUpToDate(existingSecret, secret))
        {
            oldLoadedSecrets.Remove(secretId);
            newLoadedSecrets.Add(secretId, existingSecret);
        }
        else
        {
            secretLoader.AddSecretToLoad(_options.VaultId, secret.Id);
        }
    }

    private void UpdateSecrets(FullItem[] loadedSecret, Dictionary<string, FullItem> newLoadedSecrets, Dictionary<string, FullItem> oldLoadedSecrets)
    {
        foreach (var secretBundle in loadedSecret)
        {
            newLoadedSecrets.Add(secretBundle.Id, secretBundle);
        }

        _loadedSecrets = newLoadedSecrets;

        // Reload is needed if we are loading secrets that were not loaded before or
        // secret that was loaded previously is not available anymore
        if (loadedSecret.Any() || oldLoadedSecrets?.Any() == true)
        {
            Data = _manager.GetData(newLoadedSecrets.Values);
            if (oldLoadedSecrets != null)
            {
                OnReload();
            }
        }
    }
    
    private async Task PollForSecretChangesAsync()
    {
        while (!_cancellationToken.IsCancellationRequested)
        {
            await WaitForReload().ConfigureAwait(false);
            try
            {
                await LoadAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                // Ignore
            }
        }
    }

    private Task WaitForReload() => Task.Delay(_reloadInterval!.Value, _cancellationToken.Token);

    private async Task LoadAsync()
    {
        using var secretLoader = new ParallelSecretLoader(_client);
        var newLoadedSecrets = new Dictionary<string, FullItem>();
        var oldLoadedSecrets = Interlocked.Exchange(ref _loadedSecrets, null);

        var secrets = await _client.GetVaultItemsAsync(_options.VaultId, "").ConfigureAwait(false);
        
        foreach (var secret in secrets)
        {
            AddSecretToLoader(secret, oldLoadedSecrets, newLoadedSecrets, secretLoader);
        }

        var loadedSecret = await secretLoader.WaitForAllAsync().ConfigureAwait(false);
        UpdateSecrets(loadedSecret, newLoadedSecrets, oldLoadedSecrets);
    }

    private static bool IsUpToDate(Item current, Item updated) => updated.UpdatedAt == current.UpdatedAt;

    /// <summary>
    /// Frees resources held by the <see cref="OnePasswordKeyVaultConfigurationProvider"/> object.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Frees resources held by the <see cref="OnePasswordKeyVaultConfigurationProvider"/> object.
    /// </summary>
    /// <param name="disposing">true if called from <see cref="Dispose()"/>, otherwise false.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (!_disposed)
            {
                _cancellationToken.Cancel();
                _cancellationToken.Dispose();
            }

            _disposed = true;
        }
    }
}