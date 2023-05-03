using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace OwnCloud.Controls
{
    public class ListViewCustom : ListView
    {
        private int count = 0;
        public static BindableProperty ItemClickCommandProperty = BindableProperty.Create("ItemTappedCommand",
                typeof(ICommand), typeof(ListViewCustom), null);

        private ItemTappedEventArgs eArgs;

        public ListViewCustom()
        {
            this.ItemTapped += this.OnItemTapped;
        }


        public ICommand ItemTappedCommand
        {
            get { return (ICommand)this.GetValue(ItemClickCommandProperty); }
            set { this.SetValue(ItemClickCommandProperty, value); }
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            eArgs = e;
            if (count < 1)
            {
                TimeSpan tt = new TimeSpan(0, 0, 0, 0, 800);
                Device.StartTimer(tt, doubleTap);
            }
            count++;
        }

        private bool doubleTap()
        {
            if (count > 1)
            {
                FileOwn sel = (FileOwn)eArgs.Item;
                if (sel.contentType == "dav/directory")
                {
                    Global.expandFolder += sel.name + "/";
                    Global.cloud.refresh();
                }
            }
            count = 0;
            return false;
        }
    }
}
