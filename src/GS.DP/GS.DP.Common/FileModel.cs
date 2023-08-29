using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.DP.Common;

public class FileModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public string MimeType { get; set; }
    public byte[] Content { get; set; }
    public long Size { get; set; }
}
