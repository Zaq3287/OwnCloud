using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using OwnCloud.Droid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace OwnCloud.Droid
{
    public class FileService : IFileService
    {
        public void SaveAndView(string fileName, String contentType, MemoryStream stream)
        {
            try
            {
                string root = null;
                //Get the root path in android device.
                if (Android.OS.Environment.IsExternalStorageEmulated)
                {
                    root = Android.OS.Environment.ExternalStorageDirectory.ToString();
                }
                else
                    root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

                //Create directory and file 
                Java.IO.File myDir = new Java.IO.File(root + "/Download");
                myDir.Mkdirs();

                Java.IO.File file = new Java.IO.File(myDir, fileName);

                //Remove if the file exists
                if (file.Exists()) file.Delete();

                //Write the stream into the file
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(stream.ToArray());

                outs.Flush();
                outs.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}