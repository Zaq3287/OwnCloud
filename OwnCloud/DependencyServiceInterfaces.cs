using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static Android.Provider.Settings;

namespace OwnCloud
{
    public interface IFileService
    {
        void SaveAndView(string fileName, String contentType, MemoryStream stream);
    }
}
