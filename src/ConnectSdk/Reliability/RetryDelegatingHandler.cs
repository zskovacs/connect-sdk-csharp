using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectSdk.Reliability;

/// <summary>
/// A delegating handler that provides retry functionality while executing a request.
/// </summary>
public class RetryDelegatingHandler : DelegatingHandler
{
    private readonly ReliabilitySettings _settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="RetryDelegatingHandler"/> class.
    /// </summary>
    /// <param name="settings">A ReliabilitySettings instance.</param>
    public RetryDelegatingHandler(ReliabilitySettings settings)
    {
        _settings = settings;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RetryDelegatingHandler"/> class.
    /// </summary>
    /// <param name="innerHandler">A HttpMessageHandler instance to set as the inner handler.</param>
    /// <param name="settings">A ReliabilitySettings instance.</param>
    public RetryDelegatingHandler(HttpMessageHandler innerHandler, ReliabilitySettings settings)
        : base(innerHandler)
    {
        _settings = settings;
    }

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_settings.MaximumNumberOfRetries == 0)
        {
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        HttpResponseMessage responseMessage = null;

        var numberOfAttempts = 0;
        var sent = false;

        while (!sent)
        {
            var waitFor = GetNextWaitInterval(numberOfAttempts);

            try
            {
                responseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                ThrowHttpRequestExceptionIfResponseCodeCanBeRetried(responseMessage);

                sent = true;
            }
            catch (TaskCanceledException)
            {
                numberOfAttempts++;

                if (numberOfAttempts > _settings.MaximumNumberOfRetries)
                {
                    throw new TimeoutException();
                }

                // ReSharper disable once MethodSupportsCancellation, cancel will be indicated on the token
                await Task.Delay(waitFor).ConfigureAwait(false);
            }
            catch (HttpRequestException)
            {
                numberOfAttempts++;

                if (numberOfAttempts > _settings.MaximumNumberOfRetries)
                {
                    throw;
                }

                // ReSharper disable once MethodSupportsCancellation, cancel will be indicated on the token
                await Task.Delay(waitFor).ConfigureAwait(false);
            }
        }

        return responseMessage;
    }

    private void ThrowHttpRequestExceptionIfResponseCodeCanBeRetried(HttpResponseMessage responseMessage)
    {
        if (_settings.RetryableServerErrorStatusCodes.Contains(responseMessage.StatusCode))
        {
            throw new HttpRequestException($"Http status code '{responseMessage.StatusCode}' indicates server error");
        }
    }

    private TimeSpan GetNextWaitInterval(int numberOfAttempts)
    {
        var random = new Random();

        var delta = (int)((Math.Pow(2.0, numberOfAttempts) - 1.0) *
                          random.Next(
                              (int)(_settings.DeltaBackOff.TotalMilliseconds * 0.8),
                              (int)(_settings.DeltaBackOff.TotalMilliseconds * 1.2)));

        var interval = (int)Math.Min(_settings.MinimumBackOff.TotalMilliseconds + delta, _settings.MaximumBackOff.TotalMilliseconds);

        return TimeSpan.FromMilliseconds(interval);
    }
}
