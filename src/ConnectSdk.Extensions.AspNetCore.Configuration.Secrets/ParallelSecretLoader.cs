using ConnectSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectSdk.Extensions.AspNetCore.Configuration.Secrets;

internal class ParallelSecretLoader : IDisposable
{
    private const int ParallelismLevel = 32;
    private readonly IOnePasswordConnectClient _client;
    private readonly SemaphoreSlim _semaphore;
    private readonly List<Task<FullItem>> _tasks;

    public ParallelSecretLoader(IOnePasswordConnectClient client)
    {
        _client = client;
        _semaphore = new SemaphoreSlim(ParallelismLevel, ParallelismLevel);
        _tasks = new List<Task<FullItem>>();
    }

    public void AddSecretToLoad(string vaultId, string itemId)
    {
        _tasks.Add(Task.Run(() => GetSecretAsync(vaultId, itemId)));
    }

    private async Task<FullItem> GetSecretAsync(string vaultId, string itemId)
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);
        try
        {
            return await _client.GetVaultItemByIdAsync(vaultId, itemId).ConfigureAwait(false);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public FullItem[] WaitForAll()
    {
        Task.WaitAll(_tasks.Select(t => (Task)t).ToArray());
        return _tasks.Select(t => t.Result).ToArray();
    }

    public Task<FullItem[]> WaitForAllAsync() => Task.WhenAll(_tasks);

    public void Dispose()
    {
        _semaphore?.Dispose();
    }
}