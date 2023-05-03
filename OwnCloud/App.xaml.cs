using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OwnCloud
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Cloud();
            MainPage.BindingContext = new CloudVM();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
