using Polly;
using Polly.Retry;

namespace Shared.Utilities;

public static class PollyExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static readonly Func<int, TimeSpan> RetryAttemptWaitProvider =
        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="config"></param>
    /// <returns></returns>
    public static AsyncRetryPolicy CreateDefaultPolicy(Action<PolicyBuilder> config)
    {
        var builder = Policy.Handle<TimeoutException>();
        config(builder);
        return builder.WaitAndRetryAsync(3, RetryAttemptWaitProvider);
    }
}