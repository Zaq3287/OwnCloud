using Custom.Behaviors;
using Custom.Types;
using Custom;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System;

namespace OwnCloud
{
    public class CloudVM : BindableBase
    {
        private const string davpath = "remote.php/webdav";

        private ObservableCollection<FileOwn> _lstFile;
        private FileOwn _selFile;
        private string _strUrl;
        private string _strUser;
        private string _strPassword;
        private string _path;
        private static CloudVM staticCloud = new CloudVM();

        private Client c;

        public ObservableCollection<FileOwn> lstFile { get { return _lstFile; } set { _lstFile = value; OnPropertyChanged("lstFile"); } }
        public FileOwn selFile { get { return _selFile; } set { _selFile = value; OnPropertyChanged("selFile"); } }
        public string path { get { return _path; } set { _path = value; OnPropertyChanged("path"); } }
        public string strUrl { get { return _strUrl; } set { _strUrl = value; OnPropertyChanged("strUrl"); } }
        public string strUser { get { return _strUser; } set { _strUser = value; OnPropertyChanged("strUser"); } }
        public string strPassword { get { return _strPassword; } set { _strPassword = value; OnPropertyChanged("strPassword"); } }
        public ICommand comRefresh { get; set; }
        public ICommand comDownload { get; set; }
        public ICommand comAdd { get; set; }
        public ICommand comAddFolder { get; set; }
        public ICommand comBack { get; set; }
        public ICommand comDelete { get; set; }
        public ICommand onUrlTap { get; set; }

        public CloudVM()
        {
            initCommands();

            lstFile = new ObservableCollection<FileOwn>();

            Global.expandFolder = "/";
            refresh();

            Global.cloud = this;
        }

        private void initCommands()
        {
            comRefresh = new Command(() => { refresh(); });
            comDownload = new Command(() => { download(); });
            comAdd = new Command(() => { add(); });
            comDelete = new Command(() => { delete(); });
            comAddFolder = new Command(() => { addFolder(); });
            comBack = new Command(() => { back(); });
        }

        public void refresh()
        {
            lstFile.Clear();
            selFile = null;

            Global.strUrl = "https://" + strUrl + "/owncloud";
            Global.strUser = strUser;
            Global.strPassword = strPassword;

            try
            {
                c = new Client(Global.strUrl, Global.strUser, Global.strPassword);

                var list = c.List(Global.expandFolder);

                foreach (ResourceInfo item in list)
                {
                    lstFile.Add(new FileOwn
                    {
                        contentType = item.ContentType.ToString(),
                        order = item.ContentType.Equals("dav/directory") ? 0 : 1,
                        type = item.ContentType.Equals("dav/directory") ? "folder" : item.Name.Substring(item.Name.IndexOf(".")),
                        name = item.Name.Replace("%2520", " "),
                        size = (item.Size > 1048576 ? (item.Size / 1048576).ToString() + " MB" : item.Size > 1024 ? (item.Size / 1024).ToString() + " KB" : ""),
                        modified = item.LastModified.ToString("hh:mm:dd tt", CultureInfo.InvariantCulture)
                    });
                }

                ObservableCollection<FileOwn> files = new ObservableCollection<FileOwn>(lstFile.OrderBy(x => x.order));

                lstFile = files;

                path = "Path: root" + Global.expandFolder;
            }
            catch
            {
                showMessage("Gagal terhubung!");
            }
        }

        public void download()
        {
            try
            {
                if (selFile == null) return; 

                var data = c.Download(Global.expandFolder + selFile.name);

                DependencyService.Get<IFileService>().SaveAndView(selFile.name, selFile.contentType, data);

                showMessage("File '" + selFile.name + "' berhasil didownload!");
            }
            catch
            {
                showMessage("Gagal mendownload!");
            }
            
        }

        public void back()
        {
            var arr = Global.expandFolder.Split("/".ToCharArray(), StringSplitOptions.None);
            string tmp = "";

            if (arr.Count() <= 2)
            {
                Global.expandFolder = "/";
            }
            else
            {
                for (int i = 0; i < (arr.Count() - 2); i++)
                {
                    tmp += arr[i] + "/";
                }

                Global.expandFolder = tmp;
            }

            refresh();
        }


        public async void add()
        {
            FileResult file = await FilePicker.PickAsync(null);

            if (file != null)
            {
                try
                {
                    Stream stream = await file.OpenReadAsync();

                    stream.Position = 0;

                    c.Upload(Global.expandFolder + file.FileName, stream, file.ContentType);

                    refresh();

                    showMessage("File '" + file.FileName + "' berhasil diupload!");
                }
                catch
                {
                    showMessage("Gagal upload!");
                }
                
            }
        }

        public async void addFolder()
        {
            string result = await App.Current.MainPage.DisplayPromptAsync("", "Nama folder baru");

            if (result != null)
            {
                if (!c.Exists(Global.expandFolder + result)) c.CreateDirectory(Global.expandFolder + result);

                refresh();
            }
        }

        public void delete()
        {
            try
            {
                if (selFile == null) return;

                FileOwn tmp = selFile;
                c.Delete(Global.expandFolder + selFile.name);

                refresh();

                showMessage((tmp.contentType == "dav/directory" ? "Folder" : "File") + " '" + tmp.name + "' berhasil dihapus!");
            }
            catch
            {
                showMessage("Gagal menghapus!");
            }
        }

        public static void showMessage(string strMessage)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await App.Current.MainPage.DisplayAlert("Own Cloud", strMessage, "OK");
            });
        }
    }
}
