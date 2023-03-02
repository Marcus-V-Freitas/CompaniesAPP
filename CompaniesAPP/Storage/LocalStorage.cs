namespace CompaniesAPP.Storage;

public sealed class LocalStorage : ILocalStorage
{
    private readonly IJSRuntime _jsruntime;

    public LocalStorage(IJSRuntime jSRuntime)
    {
        _jsruntime = jSRuntime;
    }

    public async Task RemoveAsync(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return;
        }

        await _jsruntime.InvokeVoidAsync("localStorage.removeItem", key);
    }

    public async Task SaveStringAsync(string key, string value)
    {
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
        {
            return;
        }

        await _jsruntime.InvokeVoidAsync("localStorage.setItem", key, value);
    }

    public async Task<string> GetStringAsync(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return null!;
        }

        return await _jsruntime.InvokeAsync<string>("localStorage.getItem", key);
    }

    public async Task SaveStringArrayAsync(string key, string[] values)
    {
        if (string.IsNullOrEmpty(key) || values == null)
        {
            return;
        }

        if (values != null)
            await _jsruntime.InvokeVoidAsync("localStorage.setItem", key, string.Join('\0', values));
    }

    public async Task<string[]> GetStringArrayAsync(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return null!;
        }

        var data = await _jsruntime.InvokeAsync<string>("localStorage.getItem", key);
        if (!string.IsNullOrEmpty(data))
            return data.Split('\0');
        return null!;
    }

    public async Task SaveObjectAsync<T>(string key, T value) where T : class
    {
        if (string.IsNullOrEmpty(key) || value == null)
        {
            return;
        }

        string json = JsonSerializer.Serialize(value);
        byte[] data = Encoding.UTF8.GetBytes(json);
        string b64 = Convert.ToBase64String(data);
        await SaveStringAsync(key, b64);
    }

    public async Task<T> GetObjectAsync<T>(string key) where T : class
    {
        if (string.IsNullOrEmpty(key))
        {
            return null!;
        }

        string b64 = await GetStringAsync(key);
        if (b64 == null)
            return null!;
        byte[] data = Convert.FromBase64String(b64);
        string json = Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<T>(json)!;
    }
}