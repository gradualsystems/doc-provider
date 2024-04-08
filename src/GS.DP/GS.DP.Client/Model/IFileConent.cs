using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.DP.Client.Model;
public interface IFileConent
{
    string FileName { get; set; }
    string Extension { get; set; }
    string ContentType { get; set; }
    long Length { get; set; }
    byte[] Content { get; set; }
}
