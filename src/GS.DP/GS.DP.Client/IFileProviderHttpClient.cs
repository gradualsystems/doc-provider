using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.DP.Client;

public interface IFileProviderHttpClient
{
    Task<Stream> DownloadFileStreamAsync(string id);

    Task<byte[]> DownloadFileBytesAsync(string id);

    //Task<List<FileDto>> UploadAsync(int id, MultipartFormDataContent content)
}
