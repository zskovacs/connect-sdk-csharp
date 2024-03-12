namespace OnePasswordConnect.Extensions.DependencyInjection;

internal class InjectableOnePasswordConnectClient : OnePasswordConnectClient
{
    public InjectableOnePasswordConnectClient(HttpClient httpClient, IOptions<OnePasswordConnectOptions> options)
        : base(httpClient, options.Value)
    {
    }
}