using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Custom
{
    public class Function
    {
        public static void showMessage(string strMessage)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Own Cloud", strMessage, "OK");
            });
        }
    }
}
