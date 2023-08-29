using System.Net.Http.Headers;
using System.Text;

namespace GS.DP.Client;

public class FileProviderHttpClient : IFileProviderHttpClient
{
    protected readonly IHttpClientFactory _httpClientFactory;

    public FileProviderHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public virtual string ClientName => this.GetType().Assembly.GetName().Name;

    public async Task<Stream> DownloadFileStreamAsync(string id)
    {
        var httpClient = _httpClientFactory.CreateClient(ClientName);

        var url = $"File/{id}";

        var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

        var response = await httpClient.SendAsync(requestMessage);

        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();

        return stream;
    }
    
    public async Task<byte[]> DownloadFileBytesAsync(string id)
    {
        var httpClient = _httpClientFactory.CreateClient(ClientName);

        var url = $"File/{id}";

        var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

        var response = await httpClient.SendAsync(requestMessage);

        response.EnsureSuccessStatusCode();

        var bytes = await response.Content.ReadAsByteArrayAsync();

        return bytes;
    }
}
