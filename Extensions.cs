using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

static public class Extensions
{
    static JsonSerializerOptions serializerOptions = new JsonSerializerOptions { WriteIndented = false };

    static public string GetJsonString<T>(this T o, JsonSerializerOptions options = null)
    {
        return JsonSerializer.Serialize<T>(o, options ?? serializerOptions);
    }

    static public T ParseJson<T>(this string json, JsonSerializerOptions options = null)
    {
        return JsonSerializer.Deserialize<T>(json, options ?? serializerOptions);
    }

    static public string Join<T>(this IEnumerable<T> data, string separator = ",")
    {
        return string.Join(separator, data.Select(x => x.ToString()).ToArray());
    }

    /// <summary>
    /// Waits asynchronously for the process to exit.
    /// </summary>
    /// <param name="process">The process to wait for cancellation.</param>
    /// <param name="cancellationToken">A cancellation token. If invoked, the task will return 
    /// immediately as canceled.</param>
    /// <returns>A Task representing waiting for the process to end.</returns>
    public static Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default(CancellationToken))
    {
        var tcs = new TaskCompletionSource<object>();
        process.EnableRaisingEvents = true;
        process.Exited += (sender, args) => tcs.TrySetResult(null);
        if (cancellationToken != default(CancellationToken))
            cancellationToken.Register(tcs.SetCanceled);

        return tcs.Task;
    }

    static public string ToUnixEOL(this string line) => line?.Replace("\r\n", "\n");
    static public async Task<string> ReadResponse(this Task<HttpResponseMessage> task)
    {
        return await (await task).Content.ReadAsStringAsync();
    }

    static public async Task<T> ReadResponse<T>(this HttpResponseMessage message)
    {
        return (await message.Content.ReadAsStringAsync()).ParseJson<T>();
    }

    static public TimeSpan TimeSince(this DateTime timestamp) => DateTime.UtcNow - timestamp.ToUniversalTime();
}