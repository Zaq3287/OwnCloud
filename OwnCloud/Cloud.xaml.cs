using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OwnCloud
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cloud : ContentPage
    {
        public Cloud()
        {
            InitializeComponent();
        }

        private void ListViewCustom_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        //    public class ListView : Xamarin.Forms.ListView
        //    {
        //        public static BindableProperty ItemClickCommandProperty = BindableProperty.Create<ListView, ICommand>(x => x.ItemClickCommand, null);

        //        public ListView()
        //        {
        //            this.ItemTapped += this.OnItemTapped;
        //        }


        //        public ICommand ItemClickCommand
        //        {
        //            get { return (ICommand)this.GetValue(ItemClickCommandProperty); }
        //            set { this.SetValue(ItemClickCommandProperty, value); }
        //        }

        //        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        //        {
        //            if (e.Item != null && this.ItemClickCommand != null && this.ItemClickCommand.CanExecute(e))
        //            {
        //                this.ItemClickCommand.Execute(e.Item);
        //                this.SelectedItem = null;
        //            }
        //        }
        //    }
        //}
    }
}