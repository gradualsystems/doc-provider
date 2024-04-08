using GS.DP.Client.Model;

namespace GS.DP.Client;

public interface IFileProviderHttpClient
{
    Task<Stream> DownloadFileStreamAsync(string id);

    Task<byte[]> DownloadFileBytesAsync(string id);

    //Task<IFileConent> DownloadFileContentAsync(string id);

    Task<string> UploadFileBytesAsync(string filename, byte[] bytes);

    Task<string> UploadFileStreamAsync(string filename, Stream stream);
}
