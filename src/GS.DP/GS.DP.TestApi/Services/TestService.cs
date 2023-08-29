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

    public async Task<int> RunTestAsync(string id)
    {
        var result = await _httpClient.DownloadFileBytesAsync(id);

        string filename = Path.Combine(Path.GetTempPath(), id);

        await File.WriteAllBytesAsync(filename, result);

        return 1;
    }
}
