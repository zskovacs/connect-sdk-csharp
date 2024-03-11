using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ConnectSdk.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddOnePasswordConnect(this IServiceCollection services, Action<IServiceProvider, OnePasswordConnectOptions> configureOptions)
    {
        services.AddOptions<OnePasswordConnectOptions>().Configure<IServiceProvider>((options, resolver) => configureOptions(resolver, options))
            .PostConfigure(options =>
            {
                if (string.IsNullOrWhiteSpace(options.ApiKey))
                {
                    throw new ArgumentNullException(nameof(options.ApiKey));
                }
            });

        services.TryAddTransient<IOnePasswordConnectClient>(resolver => resolver.GetRequiredService<InjectableOnePasswordConnectClient>());

        return services.AddHttpClient<InjectableOnePasswordConnectClient>();
    }

    public static IHttpClientBuilder AddOnePasswordConnect(this IServiceCollection services, Action<OnePasswordConnectOptions> configureOptions)
    {
        return services.AddOnePasswordConnect((_, options) => configureOptions(options));
    }
}