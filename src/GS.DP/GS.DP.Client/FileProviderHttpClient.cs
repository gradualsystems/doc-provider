using System.IO;
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

    public async Task<string> UploadFileBytesAsync(string filename, byte[] bytes)
    {
        var httpClient = _httpClientFactory.CreateClient(ClientName);

        var url = $"File";

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

        using var content = new MultipartFormDataContent();

        var fileContent = new ByteArrayContent(bytes);

        string mimeType = "application/octet-stream";

        fileContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

        content.Add(content: fileContent, name: "\"file\"", fileName: filename);

        requestMessage.Content = content;

        var response = await httpClient.SendAsync(requestMessage);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> UploadFileStreamAsync(string filename, Stream stream)
    {
        var httpClient = _httpClientFactory.CreateClient(ClientName);

        var url = $"File";

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

        using var content = new MultipartFormDataContent();

        var fileContent = new StreamContent(stream);

        string mimeType = "application/octet-stream";

        fileContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

        content.Add(content: fileContent, name: "\"file\"", fileName: filename);

        requestMessage.Content = content;

        var response = await httpClient.SendAsync(requestMessage);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
