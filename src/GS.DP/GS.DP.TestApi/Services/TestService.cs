using GS.DP.Client;
using System.Diagnostics;

namespace GS.DP.TestApi.Services;

public class TestService : ITestService
{
    private readonly IFileProviderHttpClient _httpClient;

    public TestService(IFileProviderHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> RunDownloadAsync(string id)
    {
        var result = await _httpClient.DownloadFileBytesAsync(id);

        string filename = Path.Combine(Path.GetTempPath(), id);

        await File.WriteAllBytesAsync(filename, result);

        return 1;
    }

    //public async Task<string> RunUploadAsync()
    //{
    //    string filename = @"d:\data\files\test1.pdf";

    //    using var stream = File.Open(filename, FileMode.Open, FileAccess.Read);

    //    var result = await _httpClient.UploadFileStreamAsync(Path.GetFileName(filename), stream);

    //    return result;
    //}

    public async Task<string> RunUploadAsync()
    {
        string filename = @"d:\data\files\test1.pdf";

        byte[] bytes = await File.ReadAllBytesAsync(filename);

        var result = await _httpClient.UploadFileBytesAsync(Path.GetFileName(filename), bytes);

        return result;
    }
}
