using System.Text.Json;

static public class Extensions
{
    static public JsonSerializerOptions defaultSerializerOptions = new JsonSerializerOptions { WriteIndented = false };

    static public string GetJsonString<T>(this T o, JsonSerializerOptions options = null)
    {
        return JsonSerializer.Serialize<T>(o, options ?? defaultSerializerOptions);
    }

    static public T ParseJson<T>(this string json, JsonSerializerOptions options = null)
    {
        return JsonSerializer.Deserialize<T>(json, options ?? defaultSerializerOptions);
    }

    static public string Join<T>(this IEnumerable<T> data, string separator = ",")
    {
        return string.Join(separator, data.Select(x => x?.ToString()).ToArray());
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