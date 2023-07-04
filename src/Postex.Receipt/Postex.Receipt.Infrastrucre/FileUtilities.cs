using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Postex.Receipt.Infrastrucre
{
    public static class FileUtilities
    {
        public static string GetPhysicalPath(string virtualPath)
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return Path.Combine(baseDir, virtualPath);
        }
    }
}
