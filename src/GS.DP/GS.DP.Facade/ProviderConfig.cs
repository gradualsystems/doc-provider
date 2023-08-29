using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.DP.Facade;

public class ProviderConfig
{
    public string ProviderName { get; set; }

    public string RootFolder { get; set; }

    public List<FileFormat> FileFormats { get; set; }
}
