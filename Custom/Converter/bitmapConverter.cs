using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Custom.Converter
{
    //this will return false if value is null
    [Preserve(AllMembers = true)] //avoid linker stripping this class
    public class imageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strImage;
            switch (value.ToString())
            {
                case "folder":
                    strImage = "folder.png";
                    break;

                case ".pdf":
                    strImage = "pdf.png";
                    break;

                case ".xlsx":
                    strImage = "excel.png";
                    break;

                case ".jpg":
                case ".png":
                    strImage = "image.png";
                    break;

                default:
                    strImage = "other.png";
                    break;
            }
            var imageSource = ImageSource.FromFile(strImage);
            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
