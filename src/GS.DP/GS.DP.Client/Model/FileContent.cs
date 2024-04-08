using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.DP.Client.Model;
public class FileContent : IFileConent
{
    public string FileName { get; set; }
    public string Extension { get; set; }
    public string ContentType { get; set; }
    public long Length { get; set; }
    public byte[] Content { get; set; }
}
