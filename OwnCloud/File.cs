using System;
using System.Collections.Generic;
using System.Text;

namespace OwnCloud
{
    public class FileOwn
    {
        public string contentType { get; set; }
        public int order { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string size { get; set; }
        public string modified { get; set; }
    }
}
