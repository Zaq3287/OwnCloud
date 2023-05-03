using System;
using System.Collections.Generic;
using System.Text;

namespace OwnCloud
{
    public static class Global
    {
        public static string strUrl { get; set; }
        public static string strUser { get; set; }
        public static string strPassword { get; set; }
        public static string strDavUrl { get; set; }
        public static string expandFolder { get; set; }
        public static CloudVM cloud { get; set; }
    }
}
